using McMaster.Extensions.CommandLineUtils;
using System;
using DotKube.Commands.Config;
using DotKube.Commands.Resources;

namespace DotKube
{
    class Program
    {
        public static int Main(string[] args)
        {
            var result = RunApp(args);
#if DEBUG
            Console.ReadLine();
#endif        
            return result;
        }

        static int RunApp(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "dotkube",
                Description = "Kubectl written in .Net Core",
                FullName = "Kubectl written in .Net Core"
            };

            app.HelpOption(inherited: true);

            app.Command("config", ConfigCommands.ConfigCmd);
            app.Command("get", GetCommands.GetCmd);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 1;
            });

            return app.Execute(args);
        }
    }
}
