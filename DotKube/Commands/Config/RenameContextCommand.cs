using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    [Command(Description = "Renames a context from the kubeconfig file")]
    public class RenameContextCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            Reporter.Output.WriteLine("Coming soon");
            return 0;
        }
    }
}