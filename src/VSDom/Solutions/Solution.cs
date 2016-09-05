using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VSDom.Projects;

namespace VSDom.Solutions
{
	/// <summary>Represents a Visual Studio solution file.</summary>
	public class Solution
	{
		/// <summary>Initializes a new instance of <see cref="Solution"/>.</summary>
		public Solution()
		{
			ProjectNodes = new List<ProjectNode>();
			FormatVersion = "12.00";
			VisualStudioVersion = "14.0.25123.0";
			MinimumVisualStudioVersion = "10.0.40219.1'";
			
		}

		/// <summary>The location of the solution file.</summary>
		/// <remarks>
		/// This property is set when loaded from file, and can be changed if wanted.
		/// </remarks>
		public FileInfo Location { get; set; }

		/// <summary>The format version of the solution file.</summary>
		public string FormatVersion { get; set; }
		
		/// <summary>The specific version of Visual Studio.</summary>
		public string VisualStudioVersion { get; set; }

		/// <summary>The specific major indication of the version of Visual Studio.</summary>
		public string VisualStudioMajorVersion
		{
			get
			{
				if (VisualStudioVersion == null) { return null; }
				var index = VisualStudioVersion.IndexOf('.');
				if (index == -1)
				{
					return VisualStudioVersion;
				}
				var sub = VisualStudioVersion.Substring(0, index);
				return sub;
			}
		}

		/// <summary>The minimum required Visual Studio version required to open the solution file.</summary>
		public string MinimumVisualStudioVersion { get; set; }

		/// <summary>The project nodes it contains.</summary>
		public List<ProjectNode> ProjectNodes { get; }


		/// <summary>The project it contains.</summary>
		public List<Project> Projects
		{
			get
			{
				Guard.NotNull(Location, "Location");
				var projects = new List<Project>();

				foreach (var node in ProjectNodes.Where(n => n.Path.EndsWith("proj")))
				{
					var file = Path.Combine(Location.Directory.FullName, node.Path);
					var project = Project.Load(file);
					projects.Add(project);
				}
				return projects;
			}
		}
				
		/// <summary>Represents the <see cref="Solution"/> as a <see cref="string"/>.</summary>
		public override string ToString()
		{
			var sb = new StringBuilder();
			// Start with an empty line.
			sb.AppendLine()
				.AppendFormat("Microsoft Visual Studio Solution File, Format Version {0}", FormatVersion)
				.AppendLine()
				.AppendFormat("# Visual Studio {0}", VisualStudioMajorVersion)
				.AppendLine()
				.AppendFormat("VisualStudioVersion = {0}", VisualStudioVersion)
				.AppendLine()
				.AppendFormat("MinimumVisualStudioVersion = {0}", MinimumVisualStudioVersion)
				.AppendLine();

			foreach (var project in ProjectNodes)
			{
				sb.Append(project);
			}
			return sb.ToString();
		}

		#region I/O

		/// <summary>Saves the solution to a stream.</summary>
		/// <param name="stream">
		/// The stream to safe to.
		/// </param>
		public void Save(Stream stream)
		{
			Guard.NotNull(stream, "stream");
			var writer = new StreamWriter(stream);
			writer.Write(this);
		}
		/// <summary>Saves the solution to a file.</summary>
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

		/// <summary>Saves the solution to a file.</summary>
		/// <param name="file">
		/// The file to safe to.
		/// </param>
		public void Save(string file)
		{
			Save(new FileInfo(file));
		}

		/// <summary>Loads a solution from stream.</summary>
		/// <param name="stream">
		/// The stream to load from.
		/// </param>
		public static Solution Load(Stream stream)
		{
			Guard.NotNull(stream, "stream");
			var reader = new StreamReader(stream);
			return Parse(reader);
		}

		/// <summary>Loads a solution from stream.</summary>
		/// <param name="file">
		/// The file to load from.
		/// </param>
		public static Solution Load(FileInfo file)
		{
			Guard.NotNull(file, "file");
			using (var stream = file.OpenRead())
			{
				var solution = Load(stream);
				solution.Location = file;
				return solution;
			}
		}
		/// <summary>Loads a solution from stream.</summary>
		/// <param name="file">
		/// The file to load from.
		/// </param>
		public static Solution Load(string file)
		{
			return Load(new FileInfo(file));
		}

		#endregion

		/// <summary>Parses a solution file.</summary>
		public static Solution Parse(string str)
		{
			return Parse(new StringReader(str));
		}

		private static Solution Parse(TextReader reader)
		{
			var solution = new Solution();
			string line;
			while((line = reader.ReadLine()) != null)
			{
				var match = ProjectNode.FirstLine.Match(line);
				if (match.Success)
				{
					solution.ProjectNodes.Add(ProjectNode.Create(match));
				}
			}
			return solution;
		}
	}		
}
