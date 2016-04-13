using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a reference to a file that should be compiled in a Visual Studio project file.</summary>
	public class Compile : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Compile"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Compile(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="Compile"/>.</summary>
		public override string LocalName { get { return "Compile"; } }

		/// <summary>Gets and set if it is auto generated.</summary>
		public bool? AutoGen
		{
			get { return GetBoolean("AutoGen"); }
			set { SetBoolean("AutoGen", value); }
		}

		/// <summary>Gets and set if it is design time.</summary>
		public bool? DesignTime
		{
			get { return GetBoolean("DesignTime"); }
			set { SetBoolean("DesignTime", value); }
		}

		/// <summary>Gets and set a dependency.</summary>
		public string DependentUpon
		{
			get { return Get("DependentUpon"); }
			set { Set("DependentUpon", value); }
		}

		/// <summary>Gets and set a link.</summary>
		public string Link
		{
			get { return Get("Link"); }
			set { Set("Link", value); }
		}
	}
}
