using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a group of properties in a Visual Studio project file.</summary>
	/// <remarks>
	/// In most cases, those groups are 
	/// </remarks>
	public class PropertyGroup : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="PropertyGroup"/>.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public PropertyGroup(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="PropertyGroup"/>.</summary>
		public override string LocalName { get { return "PropertyGroup"; } }

		/// <summary>Returns true if the the property group describes a specific (debug or release) configuration.</summary>
		public bool IsSpecificConfiguration { get { return IsDebugConfiguration || IsReleaseConfiguration; } }

		/// <summary>Returns true if the the property group describes a debug configuration.</summary>
		public bool IsDebugConfiguration { get { return !string.IsNullOrEmpty(Condition) && Condition.ToUpperInvariant().Contains("DEBUG"); } }

		/// <summary>Returns true if the the property group describes a release configuration.</summary>
		public bool IsReleaseConfiguration { get { return !string.IsNullOrEmpty(Condition) && Condition.ToUpperInvariant().Contains("RELEASE"); } }

		/// <summary>Gets and set the condition for the property group.</summary>
		public string Condition
		{
			get { return Get("Condition", true); }
			set { Set("Condition", value, true); }
		}
		
		/// <summary>Gets and set the debug symbols value.</summary>
		public string DebugSymbols
		{
			get { return Get("DebugSymbols"); }
			set { Set("DebugSymbols", value); }
		}

		/// <summary>Gets and set the debug type.</summary>
		public string DebugType
		{
			get { return Get("DebugType"); }
			set { Set("DebugType", value); }
		}

		/// <summary>Gets and set the defined constants.</summary>
		public string DefineConstants
		{
			get { return Get("DefineConstants"); }
			set { Set("DefineConstants", value); }
		}

		/// <summary>Gets and set the documentation file.</summary>
		public string DocumentationFile
		{
			get { return Get("DocumentationFile"); }
			set { Set("DocumentationFile", value); }
		}

		/// <summary>Gets and set the error report.</summary>
		public string ErrorReport
		{
			get { return Get("ErrorReport"); }
			set { Set("ErrorReport", value); }
		}

		/// <summary>Gets and set optimize.</summary>
		public string Optimize
		{
			get { return Get("Optimize"); }
			set { Set("Optimize", value); }
		}

		/// <summary>Gets and set the output path.</summary>
		public string OutputPath
		{
			get { return Get("OutputPath"); }
			set { Set("OutputPath", value); }
		}
		
		/// <summary>Gets and set the preference for 32 bit.</summary>
		public bool? Prefer32Bit
		{
			get { return GetBoolean("Prefer32Bit"); }
			set { SetBoolean("Prefer32Bit", value); }
		}

		/// <summary>Gets and set the warning level.</summary>
		public string WarningLevel
		{
			get { return Get("WarningLevel"); }
			set { Set("WarningLevel", value); }
		}
	}
}
