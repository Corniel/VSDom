using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;
using VSDom.Conversion;

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
            if (Element.Name != MsBuild.NS + LocalName && Element.Name != LocalName)
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
        public virtual string LocalName => GetType().Name;

        /// <summary>Gets and set an include.</summary>
        public string Include
        {
            get { return Get("Include", true); }
            set { Set("Include", value, true); }
        }

        /// <summary>Gets and set a link.</summary>
        public string Link
        {
            get { return Get("Link"); }
            set { Set("Link", value); }
        }

        /// <summary>Gets all child nodes.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "It is what it is.")]
        public ProjectFileNodeCollection<ProjectFileNode> Children { get { return GetChildren<ProjectFileNode>(); } }

        /// <summary>Gets the a <see cref="ProjectFileNodeCollection{T}"/> of children.</summary>
        public ProjectFileNodeCollection<T> GetChildren<T>() where T : ProjectFileNode
        {
            return new ProjectFileNodeCollection<T>(this);
        }

        /// <summary>Get all children.</summary>
        /// <remarks>
        /// This function exists as source for the <see cref="ProjectFileNodeCollection{T}"/>.
        /// With this construction, we can expose all children as collection.
        /// </remarks>
        internal IEnumerable<ProjectFileNode> GetAllChildren()
        {
            return Element.Elements().Select(elm => Create(elm));
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
        public string Get(string childName) { return Get(childName, false); }
        /// <summary>Gets the <see cref="string"/> value of a child element or attribute.</summary>
        public string Get(string childName, bool isAttr)
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

        /// <summary>Gets a typed value form the child node.</summary>
        protected T GetNode<T>([CallerMemberName] string propertyName = null)
        {
            var str = Get(propertyName, false);

            if (typeof(T) != typeof(string))
            {
                var converter = TypeConverters.Get(GetType(), propertyName);
                return (T)converter.ConvertFromString(str);
            }
            return (T)(object)str;
        }

        /// <summary>Sets the value of the child node.</summary>
        protected void SetNode<T>(T value, [CallerMemberName] string propertyName = null)
        {
            Set(propertyName, value == null ? null : value.ToString(), false);
        }

        /// <summary>Sets the <see cref="string"/> value of a child element.</summary>
        public void Set(string childName, string val) { Set(childName, val, false); }
        /// <summary>Sets the <see cref="string"/> value of a child element or attribute.</summary>
        public void Set(string childName, string val, bool isAttr)
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
        public bool? GetBoolean(string childName) { return GetBoolean(childName, false); }
        /// <summary>Gets the <see cref="bool"/> value of a child element or attribute.</summary>
        public bool? GetBoolean(string childName, bool isAttr)
        {
            bool val;
            var str = Get(childName, isAttr);
            if (string.IsNullOrEmpty(str) || !bool.TryParse(str, out val)) { return null; }
            return val;
        }

        /// <summary>Sets the <see cref="bool"/> value of a child element.</summary>
        public void SetBoolean(string childName, bool? val) { SetBoolean(childName, val, false); }
        /// <summary>Sets the <see cref="bool"/> value of a child element or attribute.</summary>
        public void SetBoolean(string childName, bool? val, bool isAttr)
        {
            string str = null;
            if (val.HasValue)
            {
                str = val.ToString();
            }
            Set(childName, str, isAttr);
        }

        /// <summary>Gets the <see cref="Guid"/> value of a child element.</summary>
        public Guid GetGuid(string childName) { return GetGuid(childName, false); }
        /// <summary>Gets the <see cref="Guid"/> value of a child element or attribute.</summary>
        public Guid GetGuid(string childName, bool isAttr)
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
        public void SetGuid(string childName, Guid val) { SetGuid(childName, val, false); }
        /// <summary>Sets the <see cref="Guid"/> value of a child element or attribute.</summary>
        public void SetGuid(string childName, Guid val, bool isAttr)
        {
            string str = null;
            if (val != Guid.Empty)
            {
                str = val.ToString("B").ToUpperInvariant();
            }
            Set(childName, str, isAttr);
        }

        #endregion

        /// <summary>Selects a node given an XPath expression.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">
        /// The XPath expression.
        /// </param>
        /// <remarks>
        /// Due to an issue with namespaces with an empty prefix, all references to
        /// nodes should have an x-prefix.
        /// </remarks>
        public T SelectNode<T>(string expression) where T : ProjectFileNode
        {
            var node = Element.XPathSelectElement(expression, MsBuild.Resolver);
            return (T)Create(node);
        }

        /// <summary>Selects nodes given an XPath expression.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">
        /// The XPath expression.
        /// </param>
        /// <remarks>
        /// Due to an issue with namespaces with an empty prefix, all references to
        /// nodes should have an x-prefix.
        /// </remarks>
        public IEnumerable<T> SelectNodes<T>(string expression) where T : ProjectFileNode
        {
            var nodes = Element.XPathSelectElements(expression, MsBuild.Resolver);
            return nodes.Select(node => Create(node)).Cast<T>();
        }

        /// <summary>Internal factory to allow typed navigation to children.</summary>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity",
            Justification = "It is what it is.")]
        internal static ProjectFileNode Create(XElement element)
        {
            if (element == null || element.Name.Namespace != MsBuild.NS) { return null; }

            switch (element.Name.LocalName)
            {
                case nameof(Analyzer): /*           */ return new Analyzer(element);
                case nameof(BootstrapperPackage): /**/ return new BootstrapperPackage(element);
                case nameof(ProjectExtensions): /*  */ return new ProjectExtensions(element);
                case nameof(Compile): /*            */ return new Compile(element);
                case nameof(Content): /*            */ return new Content(element);
                case nameof(EmbeddedResource): /*   */ return new EmbeddedResource(element);
                case nameof(Folder): /*             */ return new Folder(element);
                case nameof(Import): /*             */ return new Import(element);
                case nameof(ItemGroup): /*          */ return new ItemGroup(element);
                case nameof(None): /*               */ return new None(element);
                case nameof(PackageReference): /*   */ return new PackageReference(element);
                case nameof(Project): /*            */ return new Project(element);
                case nameof(ProjectReference): /*   */ return new ProjectReference(element);
                case nameof(Reference): /*          */ return new Reference(element);
                case nameof(Service): /*            */ return new Service(element);

                case nameof(PropertyGroup):
                    var first = element.Parent != null && element.Parent.Element(element.Name) == element;
                    if (first) { return new ProjectHeader(element); }
                    return new PropertyGroup(element);

                default:
                    return new UnknownNode(element);
            }
        }
    }
}
