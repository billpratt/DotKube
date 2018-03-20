using k8s;
using k8s.Models;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DotKube.Commands.Resources
{
    public partial class GetCommands
    {
        internal static void GetPodsCmd(CommandLineApplication cmd)
        {
            cmd.Description = "List all pods in ps output format";

            cmd.OnExecute(() =>
            {
                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

                V1PodList podList = new V1PodList();
                try
                {
                    IKubernetes client = new Kubernetes(config);
                    podList = client.ListNamespacedPod("default");
                }
                catch (HttpRequestException ex)
                {
                    Reporter.Output.WriteLine(ex.InnerException.Message);
                    return 1;
                }

                var getOutputList = new List<GetPodOutput>();
                foreach (var item in podList.Items)
                {
                    var totalContainers = item.Status.ContainerStatuses.Count;
                    var readyContainers = item.Status.ContainerStatuses.Count(cs => cs.Ready);
                    var restarts = item.Status.ContainerStatuses.Sum(cs => cs.RestartCount);
                    var readyString = $"{readyContainers}/{totalContainers}";

                    var getPodOutput = new GetPodOutput
                    {
                        Name = item.Metadata.Name,
                        Ready = readyString,
                        Status = item.Status.Phase,
                        Restarts = restarts,
                        Age = DateTime.UtcNow - item.Metadata.CreationTimestamp
                    };

                    getOutputList.Add(getPodOutput);
                }

                TableFormatter.Print(getOutputList, "No resources found", null, new Dictionary<string, Func<GetPodOutput, object>>
                {
                            { "NAME", x => x.Name },
                            { "READY", x => x.Ready },
                            { "STATUS", x => x.Status },
                            { "RESTARTS", x => x.Restarts },
                            { "AGE", x => x.AgeString }
                });

                return 0;

            });
        }
    }
}
