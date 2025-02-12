using ProductVersioning.Enums;
using System;
using System.Text.RegularExpressions;

namespace ProductVersioning.Helpers
{
    public static class VersionValidationHelper
    {
        public static ReleaseType ValidateReleaseType(string releaseTypeStr)
        {
            if (string.IsNullOrEmpty(releaseTypeStr) || 
                !Enum.TryParse(releaseTypeStr, true, out ReleaseType releaseType) ||
                !Enum.IsDefined(typeof(ReleaseType), releaseType))
            {
                throw new ArgumentException("Invalid release type. Use 'Feature' or 'BugFix'");
            }

            return releaseType;
        }

        public static Match ValidateVersionFormat(string version)
        {
            if (string.IsNullOrWhiteSpace(version) || 
                !Regex.IsMatch(version, @"^1\.0\.(\d+)\.(\d+)$"))
            {
                throw new ArgumentException("Invalid version format. Expected format: 1.0.<major>.<minor>");
            }

            return Regex.Match(version, @"^1\.0\.(\d+)\.(\d+)$");
        }
    }
}
