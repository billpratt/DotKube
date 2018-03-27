using k8s.KubeConfigModels;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Describe one or many contexts", ExtendedHelpText = ExtendedHelp)]
    public class GetContextsCommand : CommandBase
    {
        private const string ExtendedHelp = "\r\nExamples:\r\n" +
                                            "# List all the contexts in your kubeconfig file\r\n" +
                                            "dotkube config get-contexts\r\n" +
                                            "\r\n" +
                                            "# Describe one context in your kubeconfig file.\r\n" +
                                            "dotkube config get-contexts my-context";

        private ConfigCommand Parent { get; set; }

        [Argument(0, "context_name", Description = "[Optional] Describe one context in your kubeconfig file. Omit to see all contexts")]
        public string ContextName { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();
            var contextsToShow = config.Contexts ?? Enumerable.Empty<Context>();

            if (!contextsToShow.Any())
            {
                Reporter.Output.WriteLine("No contexts found");
                return 1;
            }

            if (!string.IsNullOrEmpty(ContextName))
            {
                var matchingContext = config.Contexts.FirstOrDefault(c => c.Name.Equals(ContextName, StringComparison.InvariantCultureIgnoreCase));
                if (matchingContext == null)
                {
                    Reporter.Output.WriteError($"No context exists with the name: \"{ContextName}\".");
                    return 1;
                }

                contextsToShow = new List<Context> { matchingContext };
            }

            TableFormatter.Print(contextsToShow, "", null, new Dictionary<string, Func<Context, object>>
            {
                        { "CURRENT", x => x.Name.Equals(config.CurrentContext, StringComparison.InvariantCultureIgnoreCase)
                                            ? "*" : "" },
                        { "NAME", x => x.Name },
                        { "CLUSTER", x => x.ContextDetails.Cluster },
                        { "AUTHINFO", x => x.ContextDetails.User },
                        { "NAMESPACE", x => string.IsNullOrEmpty(x.Namespace) ? "" : x.Namespace }
            });

            return 0;
        }

    }
}