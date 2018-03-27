using System;
using System.IO;
using System.Runtime.InteropServices;
using k8s.Exceptions;
using YamlDotNet.Serialization;
using K8SConfiguration = DotKube.K8SClient.KubeConfigModels.K8SConfiguration;

namespace DotKube.K8SClient
{
    public class KubernetesClientConfiguration
    {
        /// <summary>
        ///     kubeconfig Default Location
        /// </summary>
        private static readonly string KubeConfigDefaultLocation =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), @".kube\config")
                : Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".kube/config");

        public static K8SConfiguration GetStartingConfig(string kubeConfigPath = null)
        {
            var fileInfo = new FileInfo(kubeConfigPath ?? KubeConfigDefaultLocation);

            return LoadKubeConfig(fileInfo);
        }

        /// <summary>
        ///     Loads Kube Config
        /// </summary>
        /// <param name="kubeconfig">Kube config file contents</param>
        /// <returns>Instance of the <see cref="K8SConfiguration"/> class</returns>
        private static K8SConfiguration LoadKubeConfig(FileInfo kubeconfig)
        {
            if (!kubeconfig.Exists)
            {
                throw new KubeConfigException($"kubeconfig file not found at {kubeconfig.FullName}");
            }

            var deserializeBuilder = new DeserializerBuilder();
            var deserializer = deserializeBuilder.Build();
            using (var kubeConfigTextStream = kubeconfig.OpenText())
            {
                return deserializer.Deserialize<K8SConfiguration>(kubeConfigTextStream);
            }
        }

        /// <summary>
        ///     Loads Kube Config from string
        /// </summary>
        /// <param name="kubeconfig">Kube config file contents</param>
        /// <returns>Instance of the <see cref="K8SConfiguration"/> class</returns>
        private static K8SConfiguration LoadKubeConfig(string kubeconfig)
        {

            var deserializeBuilder = new DeserializerBuilder();
            var deserializer = deserializeBuilder.Build();
            return deserializer.Deserialize<K8SConfiguration>(kubeconfig);
        }

        /// <summary>
        ///     Loads Kube Config from stream.
        /// </summary>
        /// <param name="kubeconfig">Kube config file contents</param>
        /// <returns>Instance of the <see cref="K8SConfiguration"/> class</returns>
        private static K8SConfiguration LoadKubeConfig(Stream kubeconfig)
        {
            using (var sr = new StreamReader(kubeconfig))
            {
                var strKubeConfig = sr.ReadToEnd();
                return LoadKubeConfig(strKubeConfig);
            }
        }

        public static void WriteKubeConfig(K8SConfiguration k8sConfig, string kubeConfigPath = null)
        {
            if (k8sConfig == null)
                throw new ArgumentNullException(nameof(k8sConfig));

            var serializer = new SerializerBuilder()
                                    .Build();

            var output = serializer.Serialize(k8sConfig);
            var filePath = kubeConfigPath ?? KubeConfigDefaultLocation;

            File.WriteAllText(filePath, output);
        }
    }
}
