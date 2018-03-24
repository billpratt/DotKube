using k8s.KubeConfigModels;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    [Command(Description = "Modify kubeconfig files"),
        Subcommand("current-context", typeof(CurrentContextCommand)),
        Subcommand("delete-cluster", typeof(DeleteClusterCommand)),
        Subcommand("delete-context", typeof(DeleteContextCommand)),
        Subcommand("get-clusters", typeof(GetClustersCommand)),
        Subcommand("get-contexts", typeof(GetContextsCommand)),
        Subcommand("rename-context", typeof(RenameContextCommand)),
        Subcommand("set", typeof(SetCommand)),
        Subcommand("set-cluster", typeof(SetClusterCommand)),
        Subcommand("set-context", typeof(SetContextCommand)),
        Subcommand("set-credentials", typeof(SetCredentialsCommand)),
        Subcommand("unset", typeof(UnsetCommand)),
        Subcommand("use-context", typeof(UseContextCommand)),
        Subcommand("view", typeof(ViewCommand)),
    ]
    public class ConfigCommand : CommandBase
    {
        [Option("--kubeconfig", Description = "Path to the kubeconfig file to use for CLI requests.", Inherited = true)]
        [LegalFilePath]
        public string KubeConfigFilePath { get; set; }

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
