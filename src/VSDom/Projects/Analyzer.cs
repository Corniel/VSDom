using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a reference to an analyzer within a Visual Studio project file.</summary>
    public class Analyzer : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="Analyzer"/> node.</summary>
        /// <param name="localName">
        /// The corresponding <see cref="string"/>.
        /// </param>
        public Analyzer(string localName) : base(localName) { }

        /// <summary>Initializes a new instance of a <see cref="Analyzer"/> node.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public Analyzer(XElement element) : base(element) { }
    }
}
