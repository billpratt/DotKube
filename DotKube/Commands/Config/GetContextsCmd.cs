using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void GetContextsCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Describe one or many contexts";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("Coming soon");
            });
        }
    }
}
