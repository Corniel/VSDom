using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a bootstrapper package in a Visual Studio project file.</summary>
    public class BootstrapperPackage : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="BootstrapperPackage"/> node.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public BootstrapperPackage(XElement element) : base(element) { }

        /// <summary>Gets and set if the package is visible.</summary>
        public bool? Visible
        {
            get { return GetBoolean("Visible"); }
            set { SetBoolean("Visible", value); }
        }

        /// <summary>Gets and set if the package should be installed.</summary>
        public bool? Install
        {
            get { return GetBoolean("Install"); }
            set { SetBoolean("Install", value); }
        }

        /// <summary>Gets and set the product name.</summary>
        public string ProductName
        {
            get { return Get("ProductName"); }
            set { Set("ProductName", value); }
        }
    }
}
