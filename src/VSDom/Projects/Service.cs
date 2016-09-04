using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a service in a Visual Studio project file.</summary>
	public class Service : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Service"/>.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Service(XElement element) : base(element) { }

		/// <summary>Initializes a new instance of a <see cref="Service"/> node.</summary>
		/// <param name="include">
		/// The name/id of the service to include.
		/// </param>
		public Service(string include) : base("Service")
		{
			Include = include;
		}

		/// <summary>Gets the local name of the <see cref="Service"/>.</summary>
		public override string LocalName { get { return "Service"; } }
	}
}
