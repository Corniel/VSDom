using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VSDom.Projects;

namespace VSDom.TestTools
{
    /// <summary>Helper to write fixes on project files.</summary>
    public static class ProjectFix
    {
        private static readonly ILogger _defaultLogger = new SimpleConsoleLogger();

        /// <summary>Try to fix the <see cref="Project"/>s. Tries to <see cref="Project.Save(FileInfo)"/> those who return true.</summary>
        /// <param name="fix">
        /// The fix to apply on the project.
        /// </param>
        /// <param name="projects">
        /// The projects to fix.
        /// </param>
        /// <param name="logger">
        /// An optional custom logger.
        /// </param>
        public static void Fix(Func<Project, ILogger, bool> fix, IEnumerable<Project> projects, ILogger logger = null)
        {
            Guard.NotNull(fix, nameof(fix));
            Guard.NotNull(projects, nameof(projects));
            if (!projects.Any()) { throw new ArgumentException("No projects to fix.", nameof(projects)); }

            var log = logger ?? _defaultLogger;

            var failures = false;

            foreach(var project in projects)
            {
                try
                {
                    if(project.Location is null)
                    {
                        throw new IOException($"Can not save a project '{project?.Header?.AssemblyName}' that has no location specified.");
                    }
                    if (fix(project, log))
                    {
                        project.Save(project.Location);
                    }
                }
                catch(IOException x)
                {
                    failures = true;
                    log.LogWarning(x.Message);
                }
            }

            if(failures)
            {
                throw new AssertionException("Some project could not be safed. See output for details.");
            }
        }

        /// <summary>Projects should (under normal circumstances) not need to set
        /// the file alignment but leave it common language runtime.
        /// </summary>
        /// <remarks>
        /// see: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/filealign-compiler-option
        /// </remarks>
        public static bool FileAlignmentShouldNotBeSpecified(Project project, ILogger logger)
        {
            Guard.NotNull(project, nameof(project));

            if (project.Header.FileAlignment != null)
            {
                project.Header.FileAlignment = null;
                return true;
            }
            return false;
        }
    }
}
