using NUnit.Framework;
using System;
using System.IO;
using System.Xml.Linq;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class ProjectTest
	{
		[Test]
		public void Ctor_Null_ThrowsArgumentNullException()
		{
			var act = Assert.Throws<ArgumentNullException>(() => new Project(null));
			var exp = "element";
			Assert.AreEqual(exp, act.ParamName);
		}

		[Test]
		public void Ctor_InvalidXElement_ThrowsArgumentException()
		{
			var element = new XElement("root");
			var act = Assert.Throws<ArgumentException>(() => new Project(element));
			Assert.AreEqual(@"Invalid name. Should be 'Project'.
Parameter name: element", act.Message);
		}
		[Test]
		public void LocalName_SimpleProject_Project()
		{
			var act = TestData.GetSimpleProject().LocalName;
			var exp = "Project";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void GetToolsVersion_SimpleProject_14Dot0()
		{
			var act = TestData.GetSimpleProject().ToolsVersion;
			var exp = "14.0";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void SetToolsVersion_NewProject_UnitTestToolVersion()
		{
			var project = new Project();

			var exp = "UnitTestToolVersion";
			project.ToolsVersion = exp;
			var act = project.ToolsVersion;

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Header_NewProject_IsNull()
		{
			var project = new Project();
			var act = project.Header;
			Assert.IsNull(act);
		}

		[Test]
		public void Header_SimpleProject_IsNotNull()
		{
			var act = TestData.GetSimpleProject().Header;
			Assert.IsNotNull(act);
		}

		[Test]
		public void SignAssembly_SimpleProject_IsTrue()
		{
			var act = TestData.GetSimpleProject().Header.SignAssembly;
			Assert.IsTrue(act);
		}
		[Test]
		public void AssemblyOriginatorKeyFile_SimpleProject_VSDomSnk()
		{
			var act = TestData.GetSimpleProject().Header.AssemblyOriginatorKeyFile;
			var exp = "VSDom.snk";
			Assert.AreEqual(exp, act);
		}

		[Test]
		public void RunCodeAnalysis_SimpleProject_IsTrue()
		{
			var act = TestData.GetSimpleProject().Header.RunCodeAnalysis;
			Assert.IsTrue(act);
		}
		[Test]
		public void CodeAnalysisRuleSet_SimpleProject_VSDomRuleset()
		{
			var act = TestData.GetSimpleProject().Header.CodeAnalysisRuleSet;
			var exp = "VSDom.ruleset";
			Assert.AreEqual(exp, act);
		}


		[Test]
		public void ItemGroups_SimpleProject_2ItemGroups()
		{
			var act = TestData.GetSimpleProject().ItemGroups;
			Assert.IsNotNull(act);
			Assert.AreEqual(2, act.Count);
		}

		[Test]
		public void ToString_NewProject_SomeXmlString()
		{
			var act = new Project();
			var exp = "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" />";
			Assert.AreEqual(exp, act.ToString());
		}

		[Test]
		public void Load_FromAssemlby_IsNotNull()
		{
			using (var stream = GetType().Assembly.GetManifestResourceStream("VSDom.UnitTests.ProjectFiles.SimpleProject.xml"))
			{
				var act = Project.Load(stream);
				Assert.IsNotNull(act);
			}
		}

		/// <remarks>
		/// Test Save and Load in one test for convenience reasons.
		/// 
		/// <see cref="FileInfo.FullName"/> so both the overload with a <see cref="FileInfo"/>
		/// or a <see cref="string"/>.
		/// </remarks>
		[Test]
		public void SaveAndLoad_NewProject_FileExists()
		{
			var file = new FileInfo("Save_NewProject_FileExists.xml");

			var project = new Project();

			try
			{
				project.Save(file.FullName);
				Assert.IsTrue(file.Exists);
				Assert.IsNotNull(Project.Load(file.FullName));
			}
			finally
			{
				file.Delete();
			}

		}
	}
}
