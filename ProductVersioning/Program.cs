using ProductVersioning.Enums;
using ProductVersioning.Services;
using System;

namespace ProductVersioning
{
    class Program
    {
        private static readonly string VersionFilePath = "VersionFiles/ProductInfo.cs";

        static void Main(string[] args)
        {
            VersionService versionService = new VersionService(VersionFilePath);
            ReleaseType releaseType;
            string input = args.Length == 1 ? args[0] : GetReleaseType();

            if (!Enum.TryParse(input, true, out releaseType))
            {
                Console.WriteLine("Invalid release type. Please use 'Feature' or 'BugFix'.");
                return;
            }

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
