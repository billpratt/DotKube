using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotKube.Extensions
{
    public static class CommandOptionExtensions
    {
        public static bool GetBool(this CommandOption commandOption)
        {
            if(commandOption.HasValue())
            {
                // If we got here then the option was present in the command.

                // If the value is null, return true.
                // Example would be '--minify' without a following '=' or value
                if (string.IsNullOrEmpty(commandOption.Value()))
                    return true;

                bool.TryParse(commandOption.Value(), out var result);
                return result;
            }

            return false;
        }
    }
}
