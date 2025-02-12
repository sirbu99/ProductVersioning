using ProductVersioning.Enums;
using System;
using System.Text.RegularExpressions;

namespace ProductVersioning.Helpers
{
    public static class VersionValidationHelper
    {
        public static ReleaseType ValidateReleaseType(string releaseTypeStr)
        {
            if (!Enum.TryParse(releaseTypeStr, true, out ReleaseType releaseType))
            {
                throw new ArgumentException("Invalid release type. Use 'Feature' or 'BugFix'");
            }

            return releaseType;
        }

        public static Match ValidateVersionFormat(string version)
        {
            var match = Regex.Match(version, @"^1\.0\.(\d+)\.(\d+)$");
            if (!match.Success)
            {
                throw new ArgumentException("Invalid version format. Expected format: 1.0.<major>.<minor>");
            }
            return match;
        }
    }
}
