namespace DotKube.K8SClient.KubeConfigModels
{
    using k8s.KubeConfigModels;
    using System.Collections.Generic;
    using YamlDotNet.Serialization;

    /// <summary>
    /// kubeconfig configuration model. Holds the information needed to build connect to remote
    /// Kubernetes clusters as a given user.
    /// </summary>
    /// <remarks>
    /// Should be kept in sync with https://github.com/kubernetes/kubernetes/blob/master/staging/src/k8s.io/client-go/tools/clientcmd/api/v1/types.go
    /// </remarks>
    public class K8SConfiguration
    {
        /// <summary>
        /// Gets or sets general information to be use for CLI interactions
        /// </summary>
        [YamlMember(Alias = "preferences", Order = 6)]
        public IDictionary<string, object> Preferences { get; set; }

        [YamlMember(Alias = "apiVersion", Order = 1)]
        public string ApiVersion { get; set; }

        [YamlMember(Alias = "kind", Order = 2)]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the name of the context that you would like to use by default.
        /// </summary>
        [YamlMember(Alias = "current-context", Order = 5)]
        public string CurrentContext { get; set; }

        /// <summary>
        /// Gets or sets a map of referencable names to context configs.
        /// </summary>
        [YamlMember(Alias = "contexts", Order = 4)]
        public IEnumerable<Context> Contexts { get; set; } = new Context[0];

        /// <summary>
        /// Gets or sets a map of referencable names to cluster configs.
        /// </summary>
        [YamlMember(Alias = "clusters", Order = 3)]
        public IEnumerable<Cluster> Clusters { get; set; } = new Cluster[0];

        /// <summary>
        /// Gets or sets a map of referencable names to user configs
        /// </summary>
        [YamlMember(Alias = "users", Order = 7)]
        public IEnumerable<User> Users { get; set; } = new User[0];

        /// <summary>
        /// Gets or sets additional information. This is useful for extenders so that reads and writes don't clobber unknown fields.
        /// </summary>
        [YamlMember(Alias = "extensions", Order = 8)]
        public IDictionary<string, dynamic> Extensions { get; set; }
    }
}
