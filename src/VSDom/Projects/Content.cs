using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a file that is content within a Visual Studio project file.</summary>
	public class Content : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Content"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Content(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="Content"/>.</summary>
		public override string LocalName { get { return "Content"; } }

	}
}
