using System;
using System.IO;
using System.Reflection;

namespace TsSoft.Commons.Utils
{
    public static class AssemblyResourceHelper
    {
        public static Stream GetResourceStream(object namespaceProvider, string resourceName)
        {
            return GetResourceStream(namespaceProvider.GetType(), resourceName);
        }

        public static Stream GetResourceStream(Type namespaceProvider, string resourceName)
        {
            return GetResourceStream(namespaceProvider.Assembly, namespaceProvider.Namespace, resourceName);
        }

        public static Stream GetResourceStream(Assembly assembly, string resourceNamespace, string resourceName)
        {
            return assembly.GetManifestResourceStream(string.Format("{0}.{1}", resourceNamespace, resourceName));
        }
    }
}