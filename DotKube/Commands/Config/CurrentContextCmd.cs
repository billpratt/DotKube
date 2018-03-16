using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void CurrentContextCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Display the current context";
            cmd.OnExecute(() =>
            {
                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
                Reporter.Output.WriteLine(config.CurrentContext);
            });
        }
    }
}
