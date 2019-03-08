using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents an unknown node. So, a node that is not specified by VSDom.Projects.</summary>
    public sealed class UnknownNode : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="UnknownNode"/>.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public UnknownNode(XElement element) : base(element) { }

        /// <summary>Gets the local name of the <see cref="UnknownNode"/>.</summary>
        public override string LocalName => Element.Name.LocalName;
    }
}
