using System.Collections.Generic;
using System.Xml.Linq;

namespace VSDom.Projects
{
    /// <summary>Represents a group of properties in a Visual Studio project file.</summary>
    /// <remarks>
    /// In most cases, those groups are 
    /// </remarks>
    public class PropertyGroup : ProjectFileNode
    {
        /// <summary>Initializes a new instance of a <see cref="PropertyGroup"/>.</summary>
        /// <param name="element">
        /// The corresponding <see cref="XElement"/>.
        /// </param>
        public PropertyGroup(XElement element) : base(element) { }

        /// <summary>Returns true if the property group describes a specific (debug or release) configuration.</summary>
        public bool IsSpecificConfiguration => IsDebugConfiguration || IsReleaseConfiguration;

        /// <summary>Returns true if the property group describes a debug configuration.</summary>
        public bool IsDebugConfiguration => !string.IsNullOrEmpty(Condition) && Condition.ToUpperInvariant().Contains("DEBUG");

        /// <summary>Returns true if the property group describes a release configuration.</summary>
        public bool IsReleaseConfiguration => !string.IsNullOrEmpty(Condition) && Condition.ToUpperInvariant().Contains("RELEASE");

        /// <summary>Gets and set the condition for the property group.</summary>
        public string Condition
        {
            get => Get(nameof(Condition), true);
            set => Set(nameof(Condition), value, true);
        }

        /// <summary>Gets and set the location of the rule set.</summary>
        public string CodeAnalysisRuleSet
        {
            get => GetNode<string>();
            set => SetNode(value);
        }

        /// <summary>Gets and set the debug symbols value.</summary>
        public string DebugSymbols
        {
            get => Get(nameof(DebugSymbols));
            set => Set(nameof(DebugSymbols), value);
        }

        /// <summary>Gets and set the debug type.</summary>
        public string DebugType
        {
            get => Get(nameof(DebugType));
            set => Set(nameof(DebugType), value);
        }

        /// <summary>Gets and set the defined constants.</summary>
        public string DefineConstants
        {
            get => Get(nameof(DefineConstants));
            set => Set(nameof(DefineConstants), value);
        }

        /// <summary>Gets and set the documentation file.</summary>
        public string DocumentationFile
        {
            get => Get(nameof(DocumentationFile));
            set => Set(nameof(DocumentationFile), value);
        }

        /// <summary>Gets and set the error report.</summary>
        public string ErrorReport
        {
            get => Get(nameof(ErrorReport));
            set => Set(nameof(ErrorReport), value);
        }

        /// <summary>Gets and set optimize.</summary>
        public string Optimize
        {
            get => Get(nameof(Optimize));
            set => Set(nameof(Optimize), value);
        }

        /// <summary>Gets and set the output path.</summary>
        public string OutputPath
        {
            get => Get(nameof(OutputPath));
            set => Set(nameof(OutputPath), value);
        }

        /// <summary>Gets and set the preference for 32 bit.</summary>
        public bool? Prefer32Bit
        {
            get => GetBoolean(nameof(Prefer32Bit));
            set => SetBoolean(nameof(Prefer32Bit), value);
        }

        /// <summary>Gets and set the warning level.</summary>
        public string WarningLevel
        {
            get => Get(nameof(WarningLevel));
            set => Set(nameof(WarningLevel), value);
        }

        /// <summary>Gets and set the warnings that should be handled as Errors.</summary>
        public HashSet<int> WarningsAsErrors
        {
            get => new HashSet<int>(ParseHashSet(Get(nameof(WarningsAsErrors))));
            set => Set(nameof(WarningsAsErrors), ToXElementValue(value));
        }
        private static string ToXElementValue(HashSet<int> set)
        {
            if (set == null || set.Count == 0)
            {
                return null;
            }
            return string.Join(",", set);
        }
        private static IEnumerable<int> ParseHashSet(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                foreach (var split in str.Split(','))
                {
                    if (int.TryParse(split, out int number))
                    {
                        yield return number;
                    }
                }
            }
        }
    }
}
