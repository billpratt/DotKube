using k8s.KubeConfigModels;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotKube.Commands.Config
{
    [Command(Description = "Display clusters defined in the kubeconfig")]
    public class GetClustersCommand : CommandBase
    {
        private ConfigCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = Parent.GetK8SConfiguration();
            var clusters = config.Clusters ?? Enumerable.Empty<Cluster>();

            if(!clusters.Any())
            {
                Reporter.Output.WriteLine("No clusters found");
                return 1;
            }

            TableFormatter.Print(clusters, "", null, new Dictionary<string, Func<Cluster, object>>
            {
                { "NAME", x => x.Name }
            });
            
            return 0;
        }
    }
}