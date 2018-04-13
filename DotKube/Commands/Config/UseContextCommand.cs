using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Sets the current-context in a kubeconfig file")]
    public class UseContextCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        [Required]
        [Argument(0, "name", Description = "Context name to use")]
        public string ContextName { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();
            
            if (string.IsNullOrWhiteSpace(ContextName))
            {
                app.ShowHelp();
                Reporter.Output.WriteError("Context name is required");
                return 1;
            }

            var matchingContext = config.Contexts.FirstOrDefault(c => c.Name.Equals(ContextName, StringComparison.InvariantCultureIgnoreCase));
            if (matchingContext == null)
            {
                Reporter.Output.WriteError($"No context exists with the name: \"{ContextName}\".");
                return 1;
            }

            config.CurrentContext = ContextName;
            K8SClient.KubernetesClientConfiguration.WriteKubeConfig(config, Parent.KubeConfigFilePath);
            
            Reporter.Output.WriteLine($"Switched to context \"{ContextName}\".");

            return 0;
        }
    }
}