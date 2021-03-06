﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotKube
{
    internal class Reporter
    {
        private static readonly Reporter NullReporter = new Reporter(console: null);
        private static object _lock = new object();

        private readonly AnsiConsole _console;

        static Reporter()
        {
            Reset();
        }

        private Reporter(AnsiConsole console)
        {
            _console = console;
        }

        public static Reporter Output { get; private set; }
        public static Reporter Error { get; private set; }
        public static Reporter Verbose { get; private set; }

        /// <summary>
        /// Resets the Reporters to write to the current Console Out/Error.
        /// </summary>
        public static void Reset()
        {
            lock (_lock)
            {
                Output = new Reporter(AnsiConsole.GetOutput());
                Error = new Reporter(AnsiConsole.GetError());
                Verbose = IsVerbose ?
                    new Reporter(AnsiConsole.GetOutput()) :
                    NullReporter;
            }
        }

        public void WriteLine(string message)
        {
            lock (_lock)
            {
                if (ShouldPassAnsiCodesThrough)
                {
                    _console?.Writer?.WriteLine(message);
                }
                else
                {
                    _console?.WriteLine(message);
                }
            }
        }

        public void WriteError(string message)
        {
            WriteLine($"Error: {message}");
        }

        public void WriteWarning(string message)
        {
            WriteLine($"Warning: {message}");
        }

        public void WriteLine()
        {
            lock (_lock)
            {
                _console?.Writer?.WriteLine();
            }
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                if (ShouldPassAnsiCodesThrough)
                {
                    _console?.Writer?.Write(message);
                }
                else
                {
                    _console?.Write(message);
                }
            }
        }

        private static bool IsVerbose
        {
            get { return bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_CLI_CONTEXT_VERBOSE") ?? "false", out bool value) && value; }
        }

        private bool ShouldPassAnsiCodesThrough
        {
            get { return bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_CLI_CONTEXT_ANSI_PASS_THRU") ?? "false", out bool value) && value; }
        }
    }
}
