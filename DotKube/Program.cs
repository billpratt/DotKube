using McMaster.Extensions.CommandLineUtils;
using System;
using DotKube.Commands.Config;
using DotKube.Commands.Resources;
using DotKube.Commands;

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
            try
            {
                return CommandLineApplication.Execute<DotKubeCommand>(args);
            }
            catch (Exception ex)
            {
                Reporter.Error.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
