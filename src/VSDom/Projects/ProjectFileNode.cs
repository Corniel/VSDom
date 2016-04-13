using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents a node in a Visual Studio project file.</summary>
	public abstract class ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="ProjectFileNode"/>.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors",
			Justification = "This allows dynamic evaluation of the root element of the node.")]
		protected ProjectFileNode(XElement element)
		{
			Element = Guard.NotNull(element, "element");
			if (Element.Name != MsBuild.NS + LocalName)
			{
				throw new ArgumentException(string.Format(
					CultureInfo.InvariantCulture,
					"Invalid name. Should be '{0}'.",
					LocalName),
					"element");
			}
		}

		/// <summary>Initializes a new instance of a <see cref="ProjectFileNode"/>.</summary>
		/// <param name="localName">
		/// The local name of the node.
		/// </param>
		protected ProjectFileNode(string localName) : this(new XElement(MsBuild.NS + localName)) { }

		/// <summary>Gets the local name of the <see cref="ProjectFileNode"/>.</summary>
		public abstract string LocalName { get; }

		/// <summary>Gets the child nodes.</summary>
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
			Justification = "This is the best way to allow easy out of the box casting on children.")]
		public IEnumerable<ProjectFileNode> Children { get { return Element.Elements().Select(elm => Select(elm)); } }

		/// <summary>Gets the a <see cref="ProjectFileNodeCollection{T}"/> of children.</summary>
		public ProjectFileNodeCollection<T> GetChildren<T>() where T : ProjectFileNode
		{
			return new ProjectFileNodeCollection<T>(this);
		}

		/// <summary>Adds a child to the node.</summary>
		/// <param name="child">The child to add.</param>
		public void Add(ProjectFileNode child)
		{
			Guard.NotNull(child, "child");
			Element.Add(child.Element);
		}

		internal XElement Element { get; private set; }

		/// <summary>Represents the node as an <see cref="string"/>.</summary>
		/// <remarks>
		/// The <code>ToString()</code> of the underlying <see cref="XElement"/> is called.
		/// </remarks>
		public override string ToString() { return Element.ToString(); }

		/// <summary>Casts a <see cref="ProjectFileNode"/> to an <see cref="XElement"/>.</summary>
		public static explicit operator XElement(ProjectFileNode node) { return node == null ? null : node.Element; }

		#region Get & Set accessors

		/// <summary>Gets the <see cref="string"/> value of a child element.</summary>
		protected string Get(string childName) { return Get(childName, false); }
		/// <summary>Gets the <see cref="string"/> value of a child element or attribute.</summary>
		protected string Get(string childName, bool isAttr)
		{
			string str = null;

			if (isAttr)
			{
				var attr = Element.Attribute(childName);
				if (attr != null) { str = attr.Value; }
			}
			else
			{
				var child = Element.Element(MsBuild.NS + childName);
				if (child != null) { str = child.Value; }
			}
			return str;
		}

		/// <summary>Sets the <see cref="string"/> value of a child element.</summary>
		protected void Set(string childName, string val) { Set(childName, val, false); }
		/// <summary>Sets the <see cref="string"/> value of a child element or attribute.</summary>
		protected void Set(string childName, string val, bool isAttr)
		{
			if (isAttr)
			{
				Element.SetAttributeValue(childName, val);
			}
			else
			{
				Element.SetElementValue(MsBuild.NS + childName, val);
			}
		}

		/// <summary>Gets the <see cref="bool"/> value of a child element.</summary>
		protected bool? GetBoolean(string childName) { return GetBoolean(childName, false); }
		/// <summary>Gets the <see cref="bool"/> value of a child element or attribute.</summary>
		protected bool? GetBoolean(string childName, bool isAttr)
		{
			bool val;
			var str = Get(childName, isAttr);
			if (string.IsNullOrEmpty(str) || !bool.TryParse(str, out val)) { return null; }
			return val;
		}

		/// <summary>Sets the <see cref="bool"/> value of a child element.</summary>
		protected void SetBoolean(string childName, bool? val) { SetBoolean(childName, val, false); }
		/// <summary>Sets the <see cref="bool"/> value of a child element or attribute.</summary>
		protected void SetBoolean(string childName, bool? val, bool isAttr)
		{
			string str = null;
			if (val.HasValue)
			{
				str = val.ToString();
			}
			Set(childName, str, isAttr);
		}

		/// <summary>Gets the <see cref="Guid"/> value of a child element.</summary>
		protected Guid GetGuid(string childName) { return GetGuid(childName, false); }
		/// <summary>Gets the <see cref="Guid"/> value of a child element or attribute.</summary>
		protected Guid GetGuid(string childName, bool isAttr)
		{
			Guid val;
			var str = Get(childName, isAttr);
			if (Guid.TryParse(str, out val))
			{
				return val;
			}
			return Guid.Empty;
		}

		/// <summary>Sets the <see cref="Guid"/> value of a child element.</summary>
		protected void SetGuid(string childName, Guid val) { SetGuid(childName, val, false); }
		/// <summary>Sets the <see cref="Guid"/> value of a child element or attribute.</summary>
		protected void SetGuid(string childName, Guid val, bool isAttr)
		{
			string str = null;
			if (val != Guid.Empty)
			{
				str = val.ToString("B").ToUpperInvariant();
			}
			Set(childName, str, isAttr);
		}

		#endregion

		/// <summary>Internal select factory to allow typed navigation to children.</summary>
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity",
			Justification = "It is what it is.")]
		internal static ProjectFileNode Select(XElement element)
		{
			if (element == null || element.Name.Namespace != MsBuild.NS) { return null; }

			switch (element.Name.LocalName)
			{
				case "BootstrapperPackage": return new BootstrapperPackage(element);
				case "Compile": return new Compile(element);
				case "Content": return new Content(element);
				case "EmbeddedResource": return new EmbeddedResource(element);
				case "Folder": return new Folder(element);
				case "Import": return new Import(element);
				case "ItemGroup": return new ItemGroup(element);
				case "None": return new None(element);
				case "Project": return new Project(element);
				case "ProjectReference": return new ProjectReference(element);
				case "Reference": return new Reference(element);

				case "PropertyGroup":
					var first = element.Parent != null && element.Parent.Element(element.Name) == element;
					if (first) { return new ProjectHeader(element); }
					return new PropertyGroup(element);

				default:
					return new UnknownNode(element);
			}
		}
	}
}
