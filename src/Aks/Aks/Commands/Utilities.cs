using System;

namespace Microsoft.Azure.Commands.Aks.Commands
{
    internal static class Utilities
    {
        public static string GetParentResourceName(string parentResource)
        {
            if (string.IsNullOrWhiteSpace(parentResource))
                throw new ArgumentNullException("parentResource");

            var items = parentResource.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            return items.Length > 0 ? items[items.Length - 1] : null;
        }
    }
}
