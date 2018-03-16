using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotKube.Commands.Resources
{
    public partial class GetCommands
    {
        internal static void GetCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Display one or many resources";
            cmd.OnExecute(() =>
            {
                Reporter.Output.WriteLine("You must specify the type of resource to get.");
                cmd.ShowHelp();
                return 1;
            });

            cmd.Command("pods", GetPodsCmd);
        }
    }
}
