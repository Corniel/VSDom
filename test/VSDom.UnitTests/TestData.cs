using System.Xml.Linq;
using VSDom.Projects;

namespace VSDom.UnitTests
{
	public static class TestData
	{
		public static Project GetSimpleProject() { return  Project.Parse(ProjectFiles.SimpleProject); }
	}
}
