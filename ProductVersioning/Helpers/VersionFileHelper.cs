using System.IO;

namespace ProductVersioning.Helpers
{
    public class VersionFileHelper
    {
        public virtual string GetCurrentVersion(string versionFilePath)
        {
            if (!File.Exists(versionFilePath))
                throw new FileNotFoundException("Version file not found.");

            return File.ReadAllText(versionFilePath).Trim();
        }

        public virtual void WriteNewVersion(string versionFilePath, string newVersion)
        {
            File.WriteAllText(versionFilePath, newVersion);
        }
    }

}
