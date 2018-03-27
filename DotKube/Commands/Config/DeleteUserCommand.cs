using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Delete the specified user from the kubeconfig")]
    public class DeleteUserCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        [Required]
        [Argument(0, Name = "name", Description = "Name of the user to delete")]
        public string Name { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();

            // Cast to list in order to delete
            var users = config.Users.ToList();
            var numRemoved = users.RemoveAll(u => u.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));

            if (numRemoved == 0)
            {
                // Nothing was removed meaning the user doesn't exist
                Reporter.Output.WriteError($"No user exists with the name: \"{Name}\"");
                return 1;
            }

            // Set new list of users and save
            config.Users = users;
            K8SClient.KubernetesClientConfiguration.WriteKubeConfig(config, Parent.KubeConfigFilePath);
            
            Reporter.Output.WriteLine($"Deleted user \"{Name}\"");
            return 0;
        }
    }
}