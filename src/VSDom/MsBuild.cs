using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;

namespace VSDom
{
    /// <summary>A static class with information about MS Build.</summary>
    public static class MsBuild
    {
        /// <summary>The MS Build namespace 'http://schemas.microsoft.com/developer/msbuild/2003'.</summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "XNamespace is immutable.")]
        public static readonly XNamespace NS = XNamespace.Get(@"http://schemas.microsoft.com/developer/msbuild/2003");

        /// <summary>The MS Build XML namespace resolver.</summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "MsBuildNamespaceResolver is immutable.")]
        public static readonly IXmlNamespaceResolver Resolver = new MsBuildNamespaceResolver();
    }
}
