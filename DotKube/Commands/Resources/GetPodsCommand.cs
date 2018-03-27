using k8s;
using k8s.Models;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;

namespace DotKube.Commands.Resources
{
    [Command(Description = "List all pods in ps output format")]
    public class GetPodsCommand : CommandBase
    {
        private GetCommand Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

            var podList = new V1PodList();
            try
            {
                IKubernetes client = new Kubernetes(config);

                if (Parent.AllNamespaces)
                {
                    podList = client.ListPodForAllNamespaces();
                }
                else
                {
                    podList = client.ListNamespacedPod("default");
                }
            }
            catch (HttpRequestException ex)
            {
                Reporter.Output.WriteError(ex.InnerException.Message);
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
                    Age = DateTime.UtcNow - item.Metadata.CreationTimestamp,
                    Namespace = item.Metadata.NamespaceProperty
                };

                getOutputList.Add(getPodOutput);
            }

            var outputDict = new Dictionary<string, Func<GetPodOutput, object>>();

            if (Parent.AllNamespaces)
            {
                outputDict.Add("NAMESPACE", x => x.Namespace);
            }

            outputDict.Add("NAME", x => x.Name);
            outputDict.Add("READY", x => x.Ready);
            outputDict.Add("STATUS", x => x.Status);
            outputDict.Add("RESTARTS", x => x.Restarts);
            outputDict.Add("AGE", x => x.AgeString);

            TableFormatter.Print(getOutputList, "No resources found", null, outputDict);

            return 0;
        }
    }
}