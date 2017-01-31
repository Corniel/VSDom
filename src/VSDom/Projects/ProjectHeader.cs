using System;
using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents the project header of a Visual Studio project file.</summary>
	/// <remarks>
	/// This should be the first occurring PropertyGroup node.
	/// </remarks>
	public class ProjectHeader : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="ProjectHeader"/>.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public ProjectHeader(XElement element) : base(element) { }

		/// <summary>Gets the local name of the <see cref="ProjectHeader"/> (PropertyGroup).</summary>
		public override string LocalName { get { return "PropertyGroup"; } }

		/// <summary>Gets and set the name of the assembly.</summary>
		public string AssemblyName
		{
			get { return Get("AssemblyName"); }
			set { Set("AssemblyName", value); }
		}

		/// <summary>Gets and set the application designer folder.</summary>
		/// <remarks>
		/// This is also the location for the AssemblyInfo file.
		/// </remarks>
		public string AppDesignerFolder
		{
			get { return Get("AppDesignerFolder"); }
			set { Set("AppDesignerFolder", value); }
		}

		/// <summary>Gets and set the ID of the project.</summary>
		public Guid ProjectGuid
		{
			get { return GetGuid("ProjectGuid"); }
			set { SetGuid("ProjectGuid", value); }
		}

		/// <summary>Gets and set the output type of the project.</summary>
		public string OutputType
		{
			get { return Get("OutputType"); }
			set { Set("OutputType", value); }
		}

		/// <summary>Gets and set the target framework (version).</summary>
		public string TargetFrameworkVersion
		{
			get { return Get("TargetFrameworkVersion"); }
			set { Set("TargetFrameworkVersion", value); }
		}

		/// <summary>Gets and set the if the assembly should be signed.</summary>
		public bool? SignAssembly 
		{
			get { return GetBoolean(nameof(SignAssembly)); }
			set { SetBoolean(nameof(SignAssembly), value); }
		}

		/// <summary>Gets and set the originator of the key file.</summary>
		public string AssemblyOriginatorKeyFile
		{
			get { return Get(nameof(AssemblyOriginatorKeyFile)); }
			set { Set(nameof(AssemblyOriginatorKeyFile), value); }
		}

		/// <summary>Gets and set the if code analysis should be run at the build or not..</summary>
		public bool? RunCodeAnalysis
		{
			get { return GetBoolean(nameof(RunCodeAnalysis)); }
			set { SetBoolean(nameof(RunCodeAnalysis), value); }
		}

		/// <summary>Gets and set the location of the rule set.</summary>
		public string CodeAnalysisRuleSet
		{
			get { return Get(nameof(CodeAnalysisRuleSet)); }
			set { Set(nameof(CodeAnalysisRuleSet), value); }
		}

		/// <summary>Gets and set if the project is a coded UI test or not.</summary>
		public bool? IsCodedUITest
		{
			get { return GetBoolean("IsCodedUITest"); }
			set { SetBoolean("IsCodedUITest", value); }
		}

		/// <summary>Gets and set the type of the test project.</summary>
		public string TestProjectType
		{
			get { return Get("TestProjectType"); }
			set { Set("TestProjectType", value); }
		}

		/// <summary>Gets and set the file alignment.</summary>
		public string FileAlignment
		{
			get { return Get("FileAlignment"); }
			set { Set("FileAlignment", value); }
		}
	}
}
