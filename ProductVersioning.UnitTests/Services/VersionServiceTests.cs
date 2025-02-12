using FakeItEasy;
using FluentAssertions;
using ProductVersioning.Helpers;
using ProductVersioning.Services;
using System;
using System.IO;
using Xunit;

namespace ProductVersioning.UnitTests.Services
{
    public class VersionServiceTests
    {
        private readonly string _versionFilePath = "dummy/path/to/versionFile.txt";
        private readonly VersionService _versionService;
        private readonly VersionFileHelper _versionFileHelper = A.Fake<VersionFileHelper>();

        public VersionServiceTests()
        {
            _versionService = new VersionService(_versionFilePath, _versionFileHelper);
        }

        [Theory]
        [InlineData("1.0.20.3", "Feature", "1.0.21.0")]
        [InlineData("1.0.20.3", "BugFix", "1.0.20.4")]
        [InlineData("1.0.0.0", "Feature", "1.0.1.0")]
        [InlineData("1.0.0.0", "BugFix", "1.0.0.1")]
        public void IncrementVersion_ValidFeatureRelease_UpdatesVersion(string currentVersion, string releaseType, string expected)
        {
            // Arrange
            A.CallTo(() => _versionFileHelper.GetCurrentVersion(_versionFilePath)).Returns(currentVersion);
            A.CallTo(() => _versionFileHelper.WriteNewVersion(_versionFilePath, expected)).DoesNothing();

            // Act
            string result = _versionService.IncrementVersion(releaseType);

            // Assert
            result.Should().Be(expected);
            A.CallTo(() => _versionFileHelper.WriteNewVersion(_versionFilePath, expected)).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("InvalidVersion", "Feature")]
        [InlineData("1.0.XX.YY", "BugFix")]
        [InlineData("", "Feature")]
        public void IncrementVersion_InvalidVersionFormat_ThrowsArgumentException(string invalidVersion, string releaseType)
        {
            // Arrange
            A.CallTo(() => _versionFileHelper.GetCurrentVersion(_versionFilePath)).Returns(invalidVersion);

            // Act
            Action act = () => _versionService.IncrementVersion(releaseType);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid version format. Expected format: 1.0.<major>.<minor>");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("InvalidType")]
        public void IncrementVersion_InvalidReleaseType_ThrowsArgumentException(string invalidReleaseType)
        {
            // Arrange
            A.CallTo(() => _versionFileHelper.GetCurrentVersion(_versionFilePath)).Returns("1.0.10.5");

            // Act
            Action act = () => _versionService.IncrementVersion(invalidReleaseType);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Invalid release type. Use 'Feature' or 'BugFix'");
        }

        [Fact]
        public void IncrementVersion_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            A.CallTo(() => _versionFileHelper.GetCurrentVersion(_versionFilePath))
                .Throws<FileNotFoundException>();

            // Act
            Action act = () => _versionService.IncrementVersion("Feature");

            // Assert
            act.Should().Throw<FileNotFoundException>();
        }
    }
}
