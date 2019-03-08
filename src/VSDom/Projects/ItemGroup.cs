using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents an item group in a Visual Studio project file.</summary>
    /// <remarks>
    /// Often, an item group only contains one type of children:
    /// - Compile
    /// - Content
    /// - EmbeddedResource
    /// - Import
    /// - None
    /// - ProjectReference
    /// - Reference
    /// </remarks>
    public class ItemGroup : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="ItemGroup"/> node.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public ItemGroup(XElement element) : base(element) { }

        /// <summary>Gets the <see cref="Compile"/> children.</summary>
        public ProjectFileNodeCollection<Compile> Compiled => GetChildren<Compile>();

        /// <summary>Gets the <see cref="Content"/> children.</summary>
        public ProjectFileNodeCollection<Content> Content => GetChildren<Content>();

        /// <summary>Gets the <see cref="EmbeddedResource"/> children.</summary>
        public ProjectFileNodeCollection<EmbeddedResource> EmbeddedResources => GetChildren<EmbeddedResource>();

        /// <summary>Gets the <see cref="Import"/> children.</summary>
        /// <remarks>
        /// Imports are a way to define namespace imports/includes for a project.
        /// This feature is supported for some languages (such as VB.NET).
        /// </remarks>
        public ProjectFileNodeCollection<Import> Imports => GetChildren<Import>();

        /// <summary>Gets the <see cref="None"/> children.</summary>
        public ProjectFileNodeCollection<None> Nones => GetChildren<None>();

        /// <summary>Gets the <see cref="ProjectReference"/> children.</summary>
        public ProjectFileNodeCollection<ProjectReference> ProjectReferences => GetChildren<ProjectReference>();

        /// <summary>Gets the <see cref="Reference"/> children.</summary>
        public ProjectFileNodeCollection<Reference> References => GetChildren<Reference>();
    }
}
