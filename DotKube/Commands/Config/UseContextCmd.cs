using k8s;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;
using YamlDotNet.Serialization;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void UseContextCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Sets the current-context in a kubeconfig file";
            var contextNameArg = cmd.Argument("context_name", "Context name to use");
            cmd.OnExecute(() =>
            {
                var k8sConfig = K8SClient.KubernetesClientConfiguration.GetStartingConfig();

                var contextName = contextNameArg.Value;

                if(string.IsNullOrWhiteSpace(contextName))
                {
                    cmd.ShowHelp();
                    Reporter.Output.WriteError("Context name is required");
                    return;
                }

                var matchingContext = k8sConfig.Contexts.FirstOrDefault(c => c.Name.Equals(contextName, StringComparison.InvariantCultureIgnoreCase));
                if(matchingContext == null)
                {
                    Reporter.Output.WriteError($"No context exists with the name: \"{contextName}\".");
                    return;
                }

                k8sConfig.CurrentContext = contextName;

                K8SClient.KubernetesClientConfiguration.WriteKubeConfig(k8sConfig);
                

                Reporter.Output.WriteLine($"Switched to context \"{contextName}\".");
            });
        }
    }
}
