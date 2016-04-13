using NUnit.Framework;
using System.Linq;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class ProjectFileNodeTest
	{
		[Test]
		public void SelectNode_ReferenceToSystemXml_IsSelected()
		{
			var project = TestData.GetSimpleProject();

			var act = project.SelectNode<Reference>("//x:Reference[@Include='System.Xml']");
			var exp = project.ItemGroups[0].References[1];

			Assert.AreEqual(exp.ToString(), act.ToString());
		}

		[Test]
		public void SelectNodes_References_2Items()
		{
			var project = TestData.GetSimpleProject();

			var act = project.SelectNodes<Reference>("//x:Reference[@Include]").ToArray();
			var exp = 2;

			Assert.AreEqual(exp, act.Length);
		}
	}
}
