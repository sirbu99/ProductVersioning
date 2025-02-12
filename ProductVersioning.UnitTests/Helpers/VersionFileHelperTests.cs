using FluentAssertions;
using ProductVersioning.Helpers;
using System;
using System.IO;
using Xunit;

namespace ProductVersioning.UnitTests.Helpers
{
    public class VersionFileHelperTests
    {
        private readonly VersionFileHelper _versionFileHelper;
        private readonly string _versionFilePath = @"..\..\..\Helpers\TestVersionFiles\ProductInfo.txt";

        public VersionFileHelperTests()
        {
            _versionFileHelper = new VersionFileHelper();
        }

        [Fact]
        public void GetCurrentVersion_ShouldThrowFileNotFoundException()
        {
            // Act
            var testFilePath = "some/random/path";
            Action act = () => _versionFileHelper.GetCurrentVersion(testFilePath);

            // Assert
            act.Should().Throw<FileNotFoundException>()
                .WithMessage("Version file not found.");
        }

        [Fact]
        public void GetCurrentVersion_ShouldReturnTrimmedVersion()
        {
            // Arrange
            string expectedVersion = "1.0.2.5";
            File.WriteAllText(_versionFilePath, $" {expectedVersion} \n");

            // Act
            string result = _versionFileHelper.GetCurrentVersion(_versionFilePath);

            // Assert
            result.Should().Be(expectedVersion);
        }

        [Fact]
        public void WriteNewVersion_ShouldWriteCorrectVersionToFile()
        {
            // Arrange
            string newVersion = "1.0.3.0";

            // Act
            _versionFileHelper.WriteNewVersion(_versionFilePath, newVersion);
            string fileContent = File.ReadAllText(_versionFilePath).Trim();

            // Assert
            fileContent.Should().Be(newVersion);
        }
    }
}
