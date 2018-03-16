using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void SetCredentialsCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Sets a user entry in kubeconfig";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
