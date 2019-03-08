using System;
using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a reference to a (NuGet) package in a Visual Studio project file.</summary>
    public class PackageReference : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="PackageReference"/>.</summary>
        public PackageReference() : base(nameof(PackageReference)) { }

        /// <summary>Initializes a new instance of a <see cref="PackageReference"/>.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public PackageReference(XElement element) : base(element) { }

        /// <summary>Initializes a new instance of a <see cref="PackageReference"/>.</summary>
        public PackageReference(string include, Version version) 
            : base(nameof(PackageReference))
        {
            Include = include;
            Version = version;
        }

        /// <summary>Gets and set the version of the package.</summary>
        public Version Version
        {
            get => GetNode<Version>();
            set => SetNode(value);
        }
        /// <summary>Gets and set assets to include.</summary>
        public string IncludeAssets
        {
            get => GetNode<string>();
            set => SetNode(value);
        }
        /// <summary>Gets and set if the assets are private.</summary>
        public string PrivateAssets
        {
            get => GetNode<string>();
            set => SetNode(value);
        }
    }
}
