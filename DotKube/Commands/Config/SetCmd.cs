using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void SetCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Sets an individual value in a kubeconfig file";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
