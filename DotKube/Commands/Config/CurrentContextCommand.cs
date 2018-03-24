using k8s;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    [Command(Description = "Display the current context")]
    public class CurrentContextCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();
            Reporter.Output.WriteLine(config.CurrentContext);
            return 0;
        }
    }
}
