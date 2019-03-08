using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VSDom.Projects;

namespace VSDom.TestTools
{
    /// <summary>Helper to write assertions on project files.</summary>
    public static class ProjectAssert
    {
        private static readonly ILogger _defaultLogger = new SimpleConsoleLogger();

        /// <summary>Tests if all <see cref="Project"/>s meet the test criteria. Throws if the do not.</summary>
        /// <param name="test">
        /// The test to apply on the project.
        /// </param>
        /// <param name="projects">
        /// The projects to test.
        /// </param>
        /// <param name="logger">
        /// An optional custom logger.
        /// </param>
        public static void Test(Action<Project, ILogger> test, IEnumerable<Project> projects, ILogger logger = null)
        {
            Guard.NotNull(test, nameof(test));
            Guard.NotNull(projects, nameof(projects));
            if (!projects.Any()) { throw new ArgumentException("No projects to test.", nameof(projects)); }

            var exceptions = new List<Exception>();
            var log = logger ?? _defaultLogger;

            foreach(var project in projects)
            {
                try
                {
                    test(project, log);
                }
                catch (Exception x)
                {
                    exceptions.Add(x);
                }
            }

            if(exceptions.Count == 1)
            {
                throw exceptions[0];
            }
            if(exceptions.Any())
            {
                foreach(var x in exceptions)
                {
                    if (x is AssertionException)
                    {
                        log.LogError(x.Message);
                    }
                    else
                    {
                        log.LogError(x, string.Empty);
                    }
                }
                throw new AssertionException(string.Format("{0} projects failed. First: {1}{2}See output for more details.", exceptions.Count, exceptions[0].Message, Environment.NewLine));
            }
        }

        /// <summary>The Source Code Control properties (ProjectName, LocalPath, AuxPath, Provider) should have the value SAK.</summary>
        /// <remarks>
        /// see: https://stackoverflow.com/questions/18467994/why-missing-sccprojectname-in-project-file-cause-the-project-file-is-not-boun
        /// </remarks>
        public static void SourceCodeControlPropertiesShouldBeSet(Project project, ILogger logger)
        {
            Guard.NotNull(project, nameof(project));

            const string SAK = nameof(SAK);
            var header = project.Header;

            if (header.SccProjectName != SAK ||
                header.SccLocalPath != SAK ||
                header.SccAuxPath != SAK ||
                header.SccProvider != SAK)
            {
                Fail("All header SCC properties (SccProjectName, SccLocalPath, SccAuxPath, SccProvider) should have the value 'SAK'.", project);
            }
        }

        /// <summary>Projects should (under normal circumstances) not need to set
        /// the file alignment but leave it common language runtime.
        /// </summary>
        /// <remarks>
        /// see: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/filealign-compiler-option
        /// </remarks>
        public static void FileAlignmentShouldNotBeSpecified(Project project, ILogger logger)
        {
            Guard.NotNull(project, nameof(project));

            if(project.Header.FileAlignment != null)
            {
                Fail($"The FileAlignment is specified explicitly (value: {project.Header.FileAlignment}) in the project header.", project);
            }
        }

        /// <summary>Throws an <see cref="AssertionException"/> for the failing project.</summary>
        [DebuggerStepThrough]
        public static void Fail(string message, Project project)
        {
            Guard.NotNull(project, nameof(project));
            var formatted = $"{project?.Location?.Name ?? "{unsafed project}"}: {message}";
            throw new AssertionException(formatted);
        }
    }
}
