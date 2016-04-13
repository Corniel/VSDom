using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using VSDom.Diagnostics;

namespace VSDom.Projects
{
	/// <summary>Represents a collection of <see cref="ProjectFileNode"/>s.</summary>
	[DebuggerTypeProxy(typeof(CollectionDebugView<>)), DebuggerDisplay("{DebuggerDisplay}")]
	public class ProjectFileNodeCollection<T> : IEnumerable<T> where T : ProjectFileNode
	{
		/// <summary>Initializes a new instance of <see cref="ProjectFileNodeCollection{T}"/>.</summary>
		/// <param name="parent">
		/// The parent <see cref="ProjectFileNode"/>.
		/// </param>
		public ProjectFileNodeCollection(ProjectFileNode parent)
		{
			Parent = Guard.NotNull(parent, "parent");
		}

		/// <summary>Gets the number of items.</summary>
		public int Count { get { return Items.Count(); } }

		/// <summary>Gets the specified item.</summary>
		/// <param name="index">
		/// The index of the item.
		/// </param>
		public T this[int index]
		{
			get
			{
				return Items.Skip(index).FirstOrDefault();
			}
		}

		/// <summary>The parent 
		/// 
		/// </summary>
		private ProjectFileNode Parent { get; set; }

		/// <summary>Adds a child to the collection.</summary>
		public void Add(T child) { Parent.Add(child); }

		/// <summary>Removes a child from the collection.</summary>
		public void Remove(T child)
		{
			Guard.NotNull(child, "child");
			child.Element.Remove();
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never), ExcludeFromCodeCoverage]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} Count = {1}", typeof(T).Name, Count);
			}
		}


		#region IEnumerable

		/// <summary>Get the enumerator.</summary>
		public IEnumerator<T> GetEnumerator() { return Items.GetEnumerator(); }

		/// <summary>Get the enumerator.</summary>
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		/// <summary>Helper property to select the needed children.</summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable<T> Items { get { return Parent.Children.Where(child => child is T).Cast<T>(); } }
		
		#endregion
	}
}
