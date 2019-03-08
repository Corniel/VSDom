using System;
using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a reference to a project in a Visual Studio project file.</summary>
    public class ProjectReference : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="ProjectReference"/>.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public ProjectReference(XElement element) : base(element) { }

        /// <summary>Gets and set the name of the project.</summary>
        public string Name
        {
            get { return Get("Name"); }
            set { Set("Name", value); }
        }

        /// <summary>Gets and set the ID of the project.</summary>
        public Guid Project
        {
            get { return GetGuid("Project"); }
            set { SetGuid("Project", value); }
        }

        /// <summary>Gets and set if the reference should private (Copy local is false) or not.</summary>
        public bool? Private
        {
            get { return GetBoolean("Private"); }
            set { SetBoolean("Private", value); }
        }
    }
}
