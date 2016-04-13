using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a binary in a Visual Studio project file.</summary>
	public class Reference : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Reference"/>.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Reference(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="Reference"/>.</summary>
		public override string LocalName { get { return "Reference"; } }

		/// <summary>Gets and set the include (the name of the reference).</summary>
		public string Include
		{
			get { return Get("Include", true); }
			set { Set("Include", value, true); }
		}

		/// <summary>Gets and set the name of the reference.</summary>
		/// <remarks>
		/// This element is obsolete.
		/// </remarks>
		public string Name
		{
			get { return Get("Name"); }
			set { Set("Name", value); }
		}

		/// <summary>Gets and set the hint path.</summary>
		public string HintPath
		{
			get { return Get("HintPath"); }
			set { Set("HintPath", value); }
		}

		/// <summary>Gets and set if the reference should refer to specific version or not.</summary>
		public bool? SpecificVersion
		{
			get { return GetBoolean("SpecificVersion"); }
			set { SetBoolean("SpecificVersion", value); }
		}

		/// <summary>Gets and set if the reference should private (Copy local is false) or not.</summary>
		public bool? Private
		{
			get { return GetBoolean("Private"); }
			set { SetBoolean("Private", value); }
		}

		/// <summary>Gets and set the required target framework of the reference.</summary>
		public string RequiredTargetFramework
		{
			get { return Get("RequiredTargetFramework"); }
			set { Set("RequiredTargetFramework", value); }
		}
	}
}
