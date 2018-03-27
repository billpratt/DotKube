using McMaster.Extensions.CommandLineUtils;
using K8SConfiguration = DotKube.K8SClient.KubeConfigModels.K8SConfiguration;

namespace DotKube.Commands.Resources
{
    [Command(Description = "Display one or many resources"),
        Subcommand("pods", typeof(GetPodsCommand))
    ]
    public class GetCommand : CommandBase
    {
        [Option("--kubeconfig", Description = "Path to the kubeconfig file to use for CLI requests.", Inherited = true)]
        [LegalFilePath]
        public string KubeConfigFilePath { get; set; }

        [Option("--all-namespaces",
                 CommandOptionType.SingleOrNoValue,
                Description = "If present, list the requested object(s) across all namespaces. Namespace in current context is ignored even if specified with--namespace.",
                Inherited = true)]
        public bool AllNamespaces { get; set; }

        private DotKubeCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }

        public K8SConfiguration GetK8SConfiguration()
        {
            var k8sConfig = K8SClient.KubernetesClientConfiguration.GetStartingConfig(KubeConfigFilePath);

            return k8sConfig;
        }
    }
}
