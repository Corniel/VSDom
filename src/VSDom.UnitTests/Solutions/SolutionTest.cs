using NUnit.Framework;
using System.Linq;
using VSDom.Solutions;

namespace VSDom.UnitTests.Solutions
{
	[TestFixture]
	public class SolutionTest
	{
		[Test]
		public void Parse_SimpleSolution_Contains2Projects()
		{
			Solution solution = Solution.Parse(ProjectFiles.SimpleSolution);
			var actual = solution.ProjectNodes.ToList();
			Assert.AreEqual(4, actual.Count);
		}
	}
}
