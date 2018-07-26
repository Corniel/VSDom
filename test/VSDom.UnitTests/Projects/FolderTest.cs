using NUnit.Framework;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class FolderTest
	{
		[Test]
		public void Include_NewFolder_SomeFolderName()
		{
			var exp = "SomeFolderName";
			var act = new Folder(exp);

			Assert.AreEqual(exp, act.Include);
		}
	}
}
