using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void UseContextCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Sets the current-context in a kubeconfig file";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
