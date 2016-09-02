using System;
using System.Text;
using System.Text.RegularExpressions;

namespace VSDom.Solutions
{
	/// <summary>Represents a project in a Visual Studio solution.</summary>
	public class ProjectNode
	{
		private const string GuidPattern = @"[0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12}";
		/// <summary>Matches the firs line of a project node.</summary>
		public static readonly Regex FirstLine = new Regex(
			@"Project\(" +
			@"""{(?<type>" + GuidPattern + @")}""\) = " +
			@"""(?<name>.+)"", " +
			@"""(?<path>.+)"", " +
			@"""{(?<id>" + GuidPattern + @")}""", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>A project node type {FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}.</summary>
		public static readonly Guid ProjectType = Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");

		/// <summary>A project node type {2150E333-8FDC-42A3-9474-1A3956D46DE8}.</summary>
		public static readonly Guid FolderType = Guid.Parse("2150E333-8FDC-42A3-9474-1A3956D46DE8");

		/// <summary>The type of project.</summary>
		public Guid NodeType { get; set; }

		/// <summary>The name of the project.</summary>
		public string Name { get; set; }

		/// <summary>The (relative) path of the project.</summary>
		public string Path { get; set; }

		/// <summary>The Id (reference) of the project.</summary>
		public Guid Id { get; set; }

		/// <summary>Represents the <see cref="ProjectNode"/>  as a <see cref="string"/>.</summary>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat(@"Project(""{0:B}"") = ""{1}"", ""{2}"", ""{3:B}"")",
				NodeType, Name, Path, Id);
			sb.AppendLine();
			sb.AppendLine("EndProject");
			return sb.ToString();
		}

		internal static ProjectNode Create(Match match)
		{
			var node = new ProjectNode();
			node.NodeType = Guid.Parse(match.Groups["type"].Value);
			node.Name = match.Groups["name"].Value;
			node.Path = match.Groups["path"].Value;
			node.Id = Guid.Parse(match.Groups["id"].Value);

			return node;
		}

	}
}

