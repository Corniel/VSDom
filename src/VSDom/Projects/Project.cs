using System.IO;
using System.Xml.Linq;

namespace VSDom.Projects
{
	/// <summary>Represents the <see cref="Project"/> root node of a Visual Studio project file.</summary>
	public class Project : ProjectFileNode
	{
		/// <summary>Initializes a new instance of a <see cref="Project"/> node.</summary>
		public Project() : base("Project") { }

		/// <summary>Initializes a new instance of a <see cref="Project"/> node.</summary>
		/// <param name="element">
		/// The corresponding <see cref="XElement"/>.
		/// </param>
		public Project(XElement element) : base(element) { }

		/// <summary>The location of the File.</summary>
		/// <remarks>
		/// This property is set when loaded from file, and can be changed if wanted.
		/// </remarks>
		public FileInfo Location { get; set; }

		/// <summary>Gets the local name of the <see cref="Project"/>.</summary>
		public override string LocalName { get { return "Project"; } }

		/// <summary>Gets and se the Tools version of the project.</summary>
		public string ToolsVersion
		{
			get { return Get("ToolsVersion", true); }
			set { Set("ToolsVersion", value, true); }
		}

		/// <summary>Gets the first PropertyGroup of the project.</summary>
		public ProjectHeader Header
		{
			get
			{
				var element = Element.Element(MsBuild.NS + "PropertyGroup");
				if (element == null) { return null; }
				return new ProjectHeader(element);
			}
		}

		/// <summary>Gets a collection of <see cref="ItemGroup"/>s.</summary>
		public ProjectFileNodeCollection<ItemGroup> ItemGroups { get { return GetChildren<ItemGroup>(); } }

		/// <summary>Loads a <see cref="Project"/> from a <see cref="string"/> containing XML.</summary>
		public static Project Parse(string text)
		{
			return new Project(XElement.Parse(text));
		}

		#region I/O

		/// <summary>Saves the project to a stream.</summary>
		/// <param name="stream">
		/// The stream to safe to.
		/// </param>
		public void Save(Stream stream)
		{
			Guard.NotNull(stream, "stream");
			Element.Save(stream);
		}
		/// <summary>Saves the project to a file.</summary>
		/// <param name="file">
		/// The file to safe to.
		/// </param>
		/// <remarks>
		/// <see cref="FileStream"/> is used instead of <see cref="FileInfo.OpenWrite()"/>
		/// as that applies an append.
		/// </remarks>
		public void Save(FileInfo file)
		{
			Guard.NotNull(file, "file");
			using (var stream = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
			{
				Save(stream);
			}
		}

		/// <summary>Saves the project to a file.</summary>
		/// <param name="file">
		/// The file to safe to.
		/// </param>
		public void Save(string file)
		{
			Save(new FileInfo(file));
		}

		/// <summary>Loads a project from stream.</summary>
		/// <param name="stream">
		/// The stream to load from.
		/// </param>
		public static Project Load(Stream stream)
		{
			Guard.NotNull(stream, "stream");
			return new Project(XElement.Load(stream));
		}

		/// <summary>Loads a project from stream.</summary>
		/// <param name="file">
		/// The file to load from.
		/// </param>
		public static Project Load(FileInfo file)
		{
			Guard.NotNull(file, "file");
			using (var stream = file.OpenRead())
			{
				var project = Load(stream);
				project.Location = file;
				return project;
			}
		}
		/// <summary>Loads a project from stream.</summary>
		/// <param name="file">
		/// The file to load from.
		/// </param>
		public static Project Load(string file)
		{
			return Load(new FileInfo(file));
		}

		#endregion
	}
}
