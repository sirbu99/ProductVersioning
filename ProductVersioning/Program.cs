using ProductVersioning.Helpers;
using ProductVersioning.Services;
using System;
using System.IO;

namespace ProductVersioning
{
    class Program
    {
        private static readonly string VersionFilePath = Path.Combine(Directory.GetCurrentDirectory(), "VersionFiles", "ProductInfo.txt");

        static void Main(string[] args)
        {
            var versionFileHelper = new VersionFileHelper();
            VersionService versionService = new VersionService(VersionFilePath, versionFileHelper);
            string releaseType = args.Length == 1 ? args[0] : GetReleaseType();

            try
            {
                string newVersion = versionService.IncrementVersion(releaseType);
                Console.WriteLine($"Updated version: {newVersion}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static string GetReleaseType()
        {
            Console.Write("Enter release type (Feature/BugFix): ");
            return Console.ReadLine().Trim();
        }
    }
}
