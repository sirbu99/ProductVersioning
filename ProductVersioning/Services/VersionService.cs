using ProductVersioning.Enums;
using ProductVersioning.Helpers;
using System.IO;

namespace ProductVersioning.Services
{
    public class VersionService
    {
        private readonly string _versionFilePath;
        private readonly VersionFileHelper _versionFileHelper;

        public VersionService(string versionFilePath, VersionFileHelper versionFileHelper)
        {
            _versionFilePath = versionFilePath;
            _versionFileHelper = versionFileHelper;
        }

        public string IncrementVersion(string releaseTypeStr)
        {
            var releaseType = VersionValidationHelper.ValidateReleaseType(releaseTypeStr);

            string version = _versionFileHelper.GetCurrentVersion(_versionFilePath);
            var match = VersionValidationHelper.ValidateVersionFormat(version);

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
            _versionFileHelper.WriteNewVersion(_versionFilePath, newVersion);

            return newVersion;
        }
    }
}
