using ProductVersioning.Enums;
using System;
using System.IO;

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

        public string IncrementVersion(ReleaseType releaseType)
        {
            throw new NotImplementedException();
        }
    }
}
