using k8s;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Delete the specified cluster from the kubeconfig")]
    public class DeleteClusterCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        [Required]
        [Argument(0, Name = "name", Description = "Name of the cluster to delete")]
        public string Name { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();

            // Cast to list in order to delete
            var clusters = config.Clusters.ToList();
            var numRemoved = clusters.RemoveAll(c => c.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));

            if (numRemoved == 0)
            {
                // Nothing was removed meaning the cluster doesn't exist
                Reporter.Output.WriteError($"No cluster exists with the name: \"{Name}\"");
                return 1;
            }

            // Set new list of clusters and save
            config.Clusters = clusters;
            K8SClient.KubernetesClientConfiguration.WriteKubeConfig(config, Parent.KubeConfigFilePath);

            Reporter.Output.WriteLine($"Deleted cluster \"{Name}\"");
            return 0;
        }
    }
}
