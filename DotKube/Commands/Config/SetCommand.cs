using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    [Command(Description = "Sets an individual value in a kubeconfig file")]
    public class SetCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            Reporter.Output.WriteLine("Coming soon");
            return 0;
        }
    }
}