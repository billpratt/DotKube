using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace DotKube.Commands.Config
{
    public partial class ConfigCommands
    {
        internal static void ConfigCmd(CommandLineApplication cmd)
        {
            cmd.Description = "Modify kubeconfig files";
            cmd.OnExecute(() =>
            {
                cmd.ShowHelp();
                return 1;
            });
            
            cmd.Command("current-context", CurrentContextCmd);
            cmd.Command("delete-cluster", DeleteClusterCmd);
            cmd.Command("delete-context", DeleteContextCmd);
            cmd.Command("get-clusters", GetClustersCmd);
            cmd.Command("get-contexts", GetContextsCmd);
            cmd.Command("rename-context", RenameContextCmd);
            cmd.Command("set", SetCmd);
            cmd.Command("set-cluster", SetClusterCmd);
            cmd.Command("set-context", SetContextCmd);
            cmd.Command("set-credentials", SetCredentialsCmd);
            cmd.Command("unset", UnsetCmd);
            cmd.Command("use-context", UseContextCmd);
            cmd.Command("view", ViewCmd);
        }
    }
}
