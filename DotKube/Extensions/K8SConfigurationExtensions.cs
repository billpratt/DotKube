using DotKube.K8SClient;
using k8s.KubeConfigModels;

namespace DotKube.Extensions
{
    public static class K8SConfigurationExtensions
    {
        public static K8SConfigurationOutput ToOutputFormat(this K8SConfiguration config)
        {
            return new K8SConfigurationOutput
            {
                ApiVersion = config.ApiVersion,
                Kind = config.Kind,
                Clusters = config.Clusters,
                Contexts = config.Contexts,
                CurrentContext = config.CurrentContext,
                Preferences = config.Preferences,
                Users = config.Users,
                Extensions = config.Extensions
            };
        }
    }
}
