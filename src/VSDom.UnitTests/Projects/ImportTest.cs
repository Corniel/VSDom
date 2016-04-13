using NUnit.Framework;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class ImportTest
	{
		[Test]
		public void Project_SomeProjectName_SomeProjectName()
		{
			var import = new Import();
			var exp = "Some Project Name";

			import.Project = exp;
			var act = import.Project;

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void Project_SomeCondition_SomeCondition()
		{
			var import = new Import();
			var exp = "Some Condition";

			import.Condition = exp;
			var act = import.Condition;

			Assert.AreEqual(exp, act);
		}
	}
}
