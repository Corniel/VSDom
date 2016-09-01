using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a file that is none content within a Visual Studio project file.</summary>
	public class None : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="None"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public None(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="None"/>.</summary>
		public override string LocalName { get { return "None"; } }
	}
}
