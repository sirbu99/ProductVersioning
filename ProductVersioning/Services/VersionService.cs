using ProductVersioning.Enums;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ProductVersioning.Services
{
    public class VersionService
    {
        private readonly string _versionFilePath;

        public VersionService(string versionFilePath)
        {
            _versionFilePath = versionFilePath;
        }

        public string GetCurrentVersion()
        {
            if (!File.Exists(_versionFilePath))
                throw new FileNotFoundException("Version file not found.");

            return File.ReadAllText(_versionFilePath).Trim();
        }

        public string IncrementVersion(string releaseTypeStr)
        {
            if (!Enum.TryParse(releaseTypeStr, true, out ReleaseType releaseType))
            {
                throw new ArgumentException("Invalid release type. Use 'Feature' or 'BugFix'");
            }

            string version = GetCurrentVersion();
            var match = Regex.Match(version, @"^1\.0\.(\d+)\.(\d+)$");

            if (!match.Success)
            {
                throw new ArgumentException("Invalid version format. Expected format: 1.0.<major>.<minor>");
            }

            int major = int.Parse(match.Groups[1].Value);
            int minor = int.Parse(match.Groups[2].Value);

            switch (releaseType)
            {
                case ReleaseType.Feature:
                    major++;
                    minor = 0;
                    break;
                case ReleaseType.BugFix:
                    minor++;
                    break;
                default:
                    break;
            }

            string newVersion = $"1.0.{major}.{minor}";
            File.WriteAllText(_versionFilePath, newVersion);
            return newVersion;
        }
    }
}
