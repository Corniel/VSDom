using NUnit.Framework;
using System;
using System.Xml;

namespace VSDom.UnitTests
{
	[TestFixture]
	public class MsBuildNamespaceResolverTest
	{
		[Test]
		public void GetNamespacesInScope_All_ThrowsNotSupportedException()
		{
			Assert.Throws<NotSupportedException>(()=> MsBuild.Resolver.GetNamespacesInScope(XmlNamespaceScope.All));
		}

		[Test]
		public void LookupNamespace_RandomPrefix_SchemasMicrosoftComDeveloperMsBuild2003()
		{
			var act = MsBuild.Resolver.LookupNamespace("RandomPrefix");
			var exp = @"http://schemas.microsoft.com/developer/msbuild/2003";

			Assert.AreEqual(exp, act);
		}

		[Test]
		public void LookupPrefix_RandomNamespace_StringEmpty()
		{
			var act = MsBuild.Resolver.LookupPrefix("RandomNamespace");
			var exp = string.Empty;

			Assert.AreEqual(exp, act);
		}
	}
}
