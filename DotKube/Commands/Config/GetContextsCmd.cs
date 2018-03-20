using k8s;
using k8s.KubeConfigModels;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void GetContextsCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Describe one or many contexts";
            cmd.ExtendedHelpText = GetExampleHelp();

            var contextNameArg = cmd.Argument("context_name", "[Optional] Describe one context in your kubeconfig file. Omit to see all contexts");
            cmd.OnExecute(() =>
            {
                var k8sConfig = K8SClient.KubernetesClientConfiguration.GetStartingConfig();
                var contextsToShow = k8sConfig.Contexts;

                if(!string.IsNullOrEmpty(contextNameArg.Value))
                {
                    var matchingContext = k8sConfig.Contexts.FirstOrDefault(c => c.Name.Equals(contextNameArg.Value, StringComparison.InvariantCultureIgnoreCase));
                    if (matchingContext == null)
                    {
                        Reporter.Output.WriteError($"No context exists with the name: \"{contextNameArg.Value}\".");
                        return;
                    }

                    contextsToShow = new List<Context> { matchingContext };
                }

                TableFormatter.Print(contextsToShow, "", null, new Dictionary<string, Func<Context, object>>
                {
                            { "CURRENT", x => x.Name.Equals(k8sConfig.CurrentContext, StringComparison.InvariantCultureIgnoreCase)
                                                ? "*" : "" },
                            { "NAME", x => x.Name },
                            { "CLUSTER", x => x.ContextDetails.Cluster },
                            { "AUTHINFO", x => x.ContextDetails.User },
                            { "NAMESPACE", x => string.IsNullOrEmpty(x.Namespace) ? "" : x.Namespace }
                });
            });
        }

        private static string GetExampleHelp()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Examples:");
            sb.AppendLine("# List all the contexts in your kubeconfig file");
            sb.AppendLine("dotkube config get-contexts");
            sb.AppendLine();
            sb.AppendLine("# Describe one context in your kubeconfig file.");
            sb.AppendLine("dotkube config get-contexts my-context");

            return sb.ToString();
        }
    }
}
