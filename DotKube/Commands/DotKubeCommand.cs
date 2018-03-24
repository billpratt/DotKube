﻿using DotKube.Commands.Config;
using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

namespace DotKube.Commands
{
    [Command("dotkube",
             Description = "Kubectl written in .Net Core",
             FullName = "Kubectl written in .Net Core"),
        VersionOptionFromMember("--version", MemberName = nameof(GetVersion)),
        Subcommand("config", typeof(ConfigCommand))
    ]
    public class DotKubeCommand : CommandBase
    {
        protected override int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }

        private static string GetVersion()
            => typeof(DotKubeCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
