using NUnit.Framework;
using System;
using VSDom.Projects;

namespace VSDom.UnitTests.Projects
{
    public class PackageReferenceTest
    {
        public class ImportTest
        {
            [Test]
            public void Project_SomeProjectName_SomeProjectName()
            {
                var reference = new PackageReference("log4net", new Version(12, 3, 4));

                Assert.AreEqual("log4net", reference.Include);
                Assert.AreEqual(new Version(12, 3, 4), reference.Version);
            }
        }
    }
}
