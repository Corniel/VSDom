using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents project (file) extensions in a Visual Studio project file.</summary>
    public class ProjectExtensions : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="ProjectExtensions"/> node.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public ProjectExtensions(XElement element) : base(element) { }
    }
}
