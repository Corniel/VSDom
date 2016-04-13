using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a file that is embedded resource in a Visual Studio project file.</summary>
	public class EmbeddedResource : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="EmbeddedResource"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public EmbeddedResource(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="EmbeddedResource"/>.</summary>
		public override string LocalName { get { return "EmbeddedResource"; } }

		/// <summary>Gets and set the generator.</summary>
		public string Generator
		{
			get { return Get("Generator"); }
			set { Set("Generator", value); }
		}

		/// <summary>Gets and set the last generation output.</summary>
		public string LastGenOutput
		{
			get { return Get("LastGenOutput"); }
			set { Set("LastGenOutput", value); }
		}
	}
}
