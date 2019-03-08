using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a (most of the times empty) folder in a Visual Studio project.</summary>
    public class Folder : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="Folder"/> node.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public Folder(XElement element) : base(element) { }

        /// <summary>Initializes a new instance of a <see cref="Folder"/> node.</summary>
        /// <param name="include">
        /// The name of the folder to include.
        /// </param>
        public Folder(string include) : base("Folder")
        {
            Include = include;
        }
    }
}
