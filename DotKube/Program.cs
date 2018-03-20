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
#if DEBUG
            while (true)
            {
                var input = Prompt.GetString("> ");
                var inputSplit = input.Split(' ');

                if(inputSplit[0] == "clear")
                {
                    Console.Clear();
                    continue;
                }
                else if(inputSplit[0] == "exit")
                {
                    // Exit out
                    return 0;
                }

                var result = RunApp(inputSplit);
            }
#else
            return RunApp(args);
#endif
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

            app.Option("--kubeconfig", "Path to the kubeconfig file to use for CLI requests.", CommandOptionType.SingleValue, true);

            app.Command("config", ConfigCommands.ConfigCmd);
            app.Command("get", GetCommands.GetCmd);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 1;
            });

            try
            {
                return app.Execute(args);
            }
            catch (Exception ex)
            {
                Reporter.Output.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
