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
        public override string LocalName => nameof(PropertyGroup);
        /// <summary>Gets and set the name of the assembly.</summary>
        public string AssemblyName
        {
            get { return Get(nameof(AssemblyName)); }
            set { Set(nameof(AssemblyName), value); }
        }

        /// <summary>Gets and set the application designer folder.</summary>
        /// <remarks>
        /// This is also the location for the AssemblyInfo file.
        /// </remarks>
        public string AppDesignerFolder
        {
            get { return Get(nameof(AppDesignerFolder)); }
            set { Set(nameof(AppDesignerFolder), value); }
        }

        /// <summary>Gets and set the ID of the project.</summary>
        public Guid ProjectGuid
        {
            get { return GetGuid(nameof(ProjectGuid)); }
            set { SetGuid(nameof(ProjectGuid), value); }
        }

        /// <summary>Gets and set the output type of the project.</summary>
        public string OutputType
        {
            get { return Get(nameof(OutputType)); }
            set { Set(nameof(OutputType), value); }
        }

        /// <summary>Gets and set the target framework (version).</summary>
        public string TargetFrameworkVersion
        {
            get { return Get(nameof(TargetFrameworkVersion)); }
            set { Set(nameof(TargetFrameworkVersion), value); }
        }

        #region IIS related settings

        /// <summary>Gets and set if IIS Express should be used or not.</summary>
        public bool? UseIISExpress
        {
            get { return GetBoolean(nameof(UseIISExpress)); }
            set { SetBoolean(nameof(UseIISExpress), value); }
        }

        /// <summary>Gets and set the IISExpressSSLPort.</summary>
        public string IISExpressSSLPort
        {
            get { return Get(nameof(IISExpressSSLPort)); }
            set { Set(nameof(IISExpressSSLPort), value); }
        }

        /// <summary>Gets and set the IISExpressSSLPort.</summary>
        public string IISExpressAnonymousAuthentication
        {
            get { return Get(nameof(IISExpressAnonymousAuthentication)); }
            set { Set(nameof(IISExpressAnonymousAuthentication), value); }
        }

        /// <summary>Gets and set the IISExpressWindowsAuthentication.</summary>
        public string IISExpressWindowsAuthentication
        {
            get { return Get(nameof(IISExpressWindowsAuthentication)); }
            set { Set(nameof(IISExpressWindowsAuthentication), value); }
        }

        /// <summary>Gets and set the IISExpressUseClassicPipelineMode.</summary>
        public string IISExpressUseClassicPipelineMode
        {
            get { return Get(nameof(IISExpressUseClassicPipelineMode)); }
            set { Set(nameof(IISExpressUseClassicPipelineMode), value); }
        }
        #endregion

        /// <summary>Gets and set if the project is a coded UI test or not.</summary>

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
            get { return GetBoolean(nameof(IsCodedUITest)); }
            set { SetBoolean(nameof(IsCodedUITest), value); }
        }

        /// <summary>Gets and set if the project has NuGet packages to restore.</summary>
        public bool? RestorePackages
        {
            get { return GetBoolean(nameof(RestorePackages)); }
            set { SetBoolean(nameof(RestorePackages), value); }
        }

        /// <summary>Gets and set the NuGet's package import (time)stamp.</summary>
        public string NuGetPackageImportStamp
        {
            get => GetNode<string>();
            set => SetNode(value);
        }

        /// <summary>Gets and set the product version.</summary>
        public string ProductVersion
        {
            get => GetNode<string>();
            set => SetNode(value);
        }
        /// <summary>Gets and set the target framework profile.</summary>
        public string TargetFrameworkProfile
        {
            get => GetNode<string>();
            set => SetNode(value);
        }

        /// <summary>Gets and set the type of the test project.</summary>
        public string TestProjectType
        {
            get { return Get(nameof(TestProjectType)); }
            set { Set(nameof(TestProjectType), value); }
        }

        /// <summary>Gets and set the file alignment.</summary>
        public string FileAlignment
        {
            get { return Get(nameof(FileAlignment)); }
            set { Set(nameof(FileAlignment), value); }
        }

        /// <summary>Gets and set the restore project style.</summary>
        public string RestoreProjectStyle
        {
            get => GetNode<string>();
            set => SetNode(value);
        }
    }
}
