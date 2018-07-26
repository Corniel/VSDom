using NUnit.Framework;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class ServiceTest
	{
		[Test]
		public void Include_NewService_SomeFolderName()
		{
			var exp = "{82A7F48D-3B50-4B1E-B82E-3ADA8210C358}";
			var act = new Service(exp);

			Assert.AreEqual(exp, act.Include);
		}
	}
}
