using FluentAssertions;
using ProductVersioning.Enums;
using ProductVersioning.Helpers;
using System;
using Xunit;

namespace ProductVersioning.UnitTests.Helpers
{
    public class VersionValidationHelperTests
    {
        [Theory]
        [InlineData("Feature", ReleaseType.Feature)]
        [InlineData("BugFix", ReleaseType.BugFix)]
        [InlineData("feature", ReleaseType.Feature)] 
        [InlineData("bugfix", ReleaseType.BugFix)] 
        public void ValidateReleaseType_ValidInput_ReturnsExpectedReleaseType(string input, ReleaseType expected)
        {
            // Act
            var result = VersionValidationHelper.ValidateReleaseType(input);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("InvalidType")]
        [InlineData("")]
        [InlineData("123")]
        [InlineData(null)]
        public void ValidateReleaseType_InvalidInput_ThrowsArgumentException(string input)
        {
            // Act & Assert
            Action act = () => VersionValidationHelper.ValidateReleaseType(input);

            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("Invalid release type. Use 'Feature' or 'BugFix'");
        }

        [Theory]
        [InlineData("1.0.10.5", "10", "5")]
        [InlineData("1.0.0.1", "0", "1")]
        [InlineData("1.0.999.999", "999", "999")]
        public void ValidateVersionFormat_ValidInput_ReturnsMatch(string version, string expectedMajor, string expectedMinor)
        {
            // Act
            var match = VersionValidationHelper.ValidateVersionFormat(version);

            // Assert
            match.Success.Should().BeTrue();
            match.Groups[1].Value.Should().Be(expectedMajor);
            match.Groups[2].Value.Should().Be(expectedMinor);
        }

        [Theory]
        [InlineData("1.0.10")]
        [InlineData("2.0.10.5")]
        [InlineData("1.0.10.")]
        [InlineData("randomtext")]
        [InlineData("")]
        [InlineData(null)]
        public void ValidateVersionFormat_InvalidInput_ThrowsArgumentException(string version)
        {
            // Act
            Action act = () => VersionValidationHelper.ValidateVersionFormat(version);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("Invalid version format. Expected format: 1.0.<major>.<minor>");
        }
    }
}
