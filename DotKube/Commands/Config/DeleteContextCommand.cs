using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Delete the specified context from the kubeconfig")]
    public class DeleteContextCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        [Required]
        [Argument(0, Name = "name", Description = "Name of the context to delete")]
        public string Name { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();

            // Cast to list in order to delete
            var contexts = config.Contexts.ToList();
            var numRemoved = contexts.RemoveAll(c => c.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));

            if (numRemoved == 0)
            {
                // Nothing was removed meaning the context doesn't exist
                Reporter.Output.WriteError($"No context exists with the name: \"{Name}\"");
                return 1;
            }

            // Set new list of contexts and save
            config.Contexts = contexts;
            K8SClient.KubernetesClientConfiguration.WriteKubeConfig(config, Parent.KubeConfigFilePath);

            if (config.CurrentContext.Equals(Name, StringComparison.InvariantCultureIgnoreCase))
            {
                Reporter.Output.WriteWarning("This removed your active context, use \"dotkube config use-context\" to select a different one");
            }

            Reporter.Output.WriteLine($"Deleted context \"{Name}\"");
            return 0;
        }
    }
}