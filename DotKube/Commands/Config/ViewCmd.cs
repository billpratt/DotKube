using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void ViewCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Display merged kubeconfig settings or a specified kubeconfig file";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
