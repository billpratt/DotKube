﻿using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    [Command(Description = "Sets a context entry in kubeconfig")]
    public class SetContextCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            Reporter.Output.WriteLine("Coming soon");
            return 0;
        }
    }
}