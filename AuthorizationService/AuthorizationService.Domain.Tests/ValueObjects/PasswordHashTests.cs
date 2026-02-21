using AuthorizationService.Domain.Shared.Exceptions;
using AuthorizationService.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace AuthorizationService.Domain.Tests.ValueObjects
{
    public class PasswordHashTests
    {
        [Theory]
        [InlineData("AQAAAAEAACcQAAAAEJxT")]
        public void Create_WithValidHash_ReturnsEmailAddress(string hash)
        {
            // Act
            var result = PasswordHash.Create(hash);

            // Assert
            result.Hash.Should().Be(hash);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Create_WithInvalidHash_ThrowsDomainException(string hash)
        {
            // Act
            var result = () => PasswordHash.Create(hash);

            // Assert
            result.Should()
                  .Throw<DomainException>()
                  .WithMessage("Password hash cannot be null or empty.");
        }
    }
}
