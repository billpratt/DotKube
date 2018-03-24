using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotKube.Commands
{
    /// <summary>
    /// This base type provides shared functionality.
    /// Also, declaring <see cref="HelpOptionAttribute"/> on this type means all types that inherit from it
    /// will automatically support '--help'
    /// </summary>
    [HelpOption]
    public abstract class CommandBase
    {
        protected virtual int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();

            return 0;
        }
    }
}
