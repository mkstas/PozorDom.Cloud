using AuthorizationService.Domain.Shared.Exceptions;
using AuthorizationService.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace AuthorizationService.Domain.Tests.ValueObjects
{
    public class EmailAddressTests
    {
        [Theory]
        [InlineData("valid@example.com")]
        public void Create_WithValidEmail_ReturnsEmailAddress(string address)
        {
            // Act
            var result = EmailAddress.Create(address);

            // Assert
            result.Address.Should().Be(address);
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("@example.com")]
        [InlineData("@example")]
        public void Create_WithInvalidEmail_ThrowsDomainException(string address)
        {
            // Act
            var result = () => EmailAddress.Create(address);

            // Assert
            result.Should()
                  .Throw<DomainException>()
                  .WithMessage("Invalid email address format.");
        }

        [Theory]
        [InlineData(" whitespace @example.com")]
        public void Create_WithWhitespaces_ThrowsDomainException(string address)
        {
            // Act
            var result = () => EmailAddress.Create(address);

            // Assert
            result.Should()
                  .Throw<DomainException>()
                  .WithMessage("Email address must not contain whitespace characters.");
        }

        [Fact]
        public void Create_WithTooLongEmail_ThrowsDomainException()
        {
            // Arrange
            var address = new string('a', EmailAddress.MAX_ADDRESS_LENGTH) + "@example.com";

            // Act
            var result = () => EmailAddress.Create(address);

            // Assert
            result.Should()
                  .Throw<DomainException>()
                  .WithMessage($"Email address must not exceed {EmailAddress.MAX_ADDRESS_LENGTH} characters.");
        }

        [Fact]
        public void Create_WithMaxLengthEmail_ReturnsEmailAddress()
        {
            // Arrange
            var address = new string('a', EmailAddress.MAX_ADDRESS_LENGTH - "@example.com".Length) + "@example.com";

            // Act
            var result = EmailAddress.Create(address);

            // Assert
            result.Address.Should().Be(address);
        }
    }
}
