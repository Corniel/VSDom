using System;
using System.Collections.Generic;
using System.Xml;

namespace VSDom
{
    /// <summary>MsBuild namespace resolver.</summary>
    public class MsBuildNamespaceResolver : IXmlNamespaceResolver
    {
        /// <summary>Gets the namespaces in scope.</summary>
        /// <remarks>
        /// Is not supported for this implementation.
        /// </remarks>
        public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope) { throw new NotSupportedException(); }

        /// <summary>Gets the namespace corresponding to the prefix.</summary>
        /// <returns>
        /// The <see cref="MsBuild.NS"/> namespace name.
        /// </returns>
        public string LookupNamespace(string prefix) { return MsBuild.NS.NamespaceName; }

        /// <summary>Gets the prefix based on the namespace name.</summary>
        /// <returns>
        /// <see cref="string.Empty"/>.
        /// </returns>
        public string LookupPrefix(string namespaceName) { return string.Empty; }
    }
}
