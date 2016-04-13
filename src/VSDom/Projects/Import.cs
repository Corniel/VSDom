using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents an <see cref="Import"/> statement in a Visual Studio project.</summary>
	public class Import : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Import"/> node.</summary>
		public Import() : base("Import") { }

		/// <summary>Initializes a new instance of a <see cref="Import"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Import(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="Import"/>.</summary>
		public override string LocalName { get { return "Import"; } }

		/// <summary>Gets and set the project to import.</summary>
		public string Project
		{
			get { return Get("Project", true); }
			set { Set("Project", value, true); }
		}


		/// <summary>Gets and set the condition for the import.</summary>
		public string Condition
		{
			get { return Get("Condition", true); }
			set { Set("Condition", value, true); }
		}
	}
}
