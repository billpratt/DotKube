using DotKube.Extensions;
using k8s.KubeConfigModels;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace DotKube.Commands.Config
{
    [Command(Description = "Display merged kubeconfig settings or a specified kubeconfig file")]
    public class ViewCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        // TODO: This should be true if --minify is passed without a value
        [Option("--minify", "Remove all information not used by current-context from the output", CommandOptionType.SingleOrNoValue)]
        public bool Minify { get; set; }

        [Option("-o|--output", "Output format. One of: json|yaml", CommandOptionType.SingleValue)]
        public string Output { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = K8SClient.KubernetesClientConfiguration.GetStartingConfig();
            
            var configToShow = config;
            if (Minify)
            {
                configToShow = MinifiedConfig(config);
            }

            var output = "yaml";
            if (!string.IsNullOrWhiteSpace(Output))
            {
                output = Output.ToLower();
            }

            // Redact sensitive data
            RedactCertificateData(configToShow);

            switch (output)
            {
                case "json":
                    PrintJson(configToShow);
                    break;
                default:
                    PrintYaml(configToShow);
                    break;
            }

            return 0;
        }

        private static K8SConfiguration MinifiedConfig(K8SConfiguration k8sConfig)
        {
            if (k8sConfig == null)
                throw new ArgumentNullException(nameof(k8sConfig));

            var currentContext = k8sConfig.Contexts.First(c => c.Name == k8sConfig.CurrentContext);
            var minifiedK8sConfig = new K8SConfiguration
            {
                Preferences = k8sConfig.Preferences,
                ApiVersion = k8sConfig.ApiVersion,
                Kind = k8sConfig.Kind,
                CurrentContext = k8sConfig.CurrentContext,
                Contexts = new List<Context> { currentContext },
                Clusters = k8sConfig.Clusters.Where(c => c.Name == currentContext.ContextDetails.Cluster),
                Users = k8sConfig.Users.Where(u => u.Name == currentContext.ContextDetails.User)
            };

            return minifiedK8sConfig;
        }

        private static void RedactCertificateData(K8SConfiguration k8sConfig)
        {
            foreach (var cluster in k8sConfig.Clusters)
            {
                if (!string.IsNullOrWhiteSpace(cluster.ClusterEndpoint?.CertificateAuthorityData))
                {
                    cluster.ClusterEndpoint.CertificateAuthorityData = "REDACTED";
                }
            }

            foreach (var user in k8sConfig.Users)
            {
                if (!string.IsNullOrWhiteSpace(user.UserCredentials?.ClientCertificateData))
                {
                    user.UserCredentials.ClientCertificateData = "REDACTED";
                }
                if (!string.IsNullOrWhiteSpace(user.UserCredentials?.ClientKeyData))
                {
                    user.UserCredentials.ClientKeyData = "REDACTED";
                }
            }
        }

        private static void PrintYaml(K8SConfiguration k8sConfig)
        {
            var serializer = new SerializerBuilder()
                                .Build();

            var output = serializer.Serialize(k8sConfig.ToOutputFormat());

            Reporter.Output.Write(output);
        }

        private static void PrintJson(K8SConfiguration k8sConfig)
        {
            var serializer = new SerializerBuilder()
                                .JsonCompatible()
                                .Build();

            var output = serializer.Serialize(k8sConfig.ToOutputFormat());

            // Prettify JSON
            output = JToken.Parse(output).ToString(Newtonsoft.Json.Formatting.Indented);

            Reporter.Output.Write(output);
        }
    }
}