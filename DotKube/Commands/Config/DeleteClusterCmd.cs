using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void DeleteClusterCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Delete the specified cluster from the kubeconfig";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
