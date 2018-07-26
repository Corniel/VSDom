using NUnit.Framework;
using VSDom.Projects;
using System.Linq;

namespace VSDom.UnitTests.Projects
{
	[TestFixture]
	public class ProjectFileNodeCollectionTest
	{
		[Test]
		public void Add_1Item_CountIs1()
		{
			var act = new ProjectFileNodeCollection<Folder>(new Project());
			var exp = "SomeFolder";

			Assert.AreEqual(0, act.Count, "There should be no items before adding a folder.");

			act.Add(new Folder(exp));

			Assert.AreEqual(1, act.Count, "There should be one item after adding a folder.");
			Assert.AreEqual(exp, act[0].Include);
		}

		[Test]
		public void Remove_1Item_CountIs1()
		{
			var act = TestData.GetSimpleProject().ItemGroups;

			Assert.AreEqual(2, act.Count, "There should be two items before removing an item group.");
			act.Remove(act[0]);
			Assert.AreEqual(1, act.Count, "There should be one item after adding an item group.");
		}

		[Test]
		public void Enumerator_EmptyCollection_CountIs0()
		{
			var collection = new ProjectFileNodeCollection<Folder>(new Project());
			var act = collection.ToList();

			Assert.AreEqual(0, act.Count);
		}
	}
}
