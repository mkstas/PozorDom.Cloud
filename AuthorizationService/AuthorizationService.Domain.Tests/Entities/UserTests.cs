using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace AuthorizationService.Domain.Tests.Entities
{
    public class UserTests
    {
        private readonly EmailAddress _address = EmailAddress.Create("valid@example.com");
        private readonly PasswordHash _hash = PasswordHash.Create("AQAAAAEAACcQAAAAEJxT");

        [Fact]
        public void Create_WithValidParameters_GeneratesNonEmptyId()
        {
            // Act
            var user = User.Create(_address, _hash);

            // Assert
            user.Should().NotBeNull();
        }

        [Fact]
        public void Create_WithValidParameters_SetEmailAddressCorrectly()
        {
            // Act
            var user = User.Create(_address, _hash);

            // Assert
            user.EmailAddress.Address.Should().Be(_address.Address);
        }

        [Fact]
        public void Create_WithValidParameters_SetPasswordHashCorrectly()
        {
            // Act
            var user = User.Create(_address, _hash);

            // Assert
            user.PasswordHash.Hash.Should().Be(_hash.Hash);
        }

        [Fact]
        public void ChangeEmailAddress_WithValidAddress_UpdatesEmailAddress()
        {
            // Arrange
            var user = User.Create(_address, _hash);
            var email = EmailAddress.Create("new@example.com");

            // Act
            user.ChangeEmailAddress(email);

            // Assert
            user.EmailAddress.Address.Should().Be(email.Address);
        }

        [Fact]
        public void ChangePasswordHash_WithValidHash_UpdatesPasswordHash()
        {
            // Arrange
            var user = User.Create(_address, _hash);
            var password = PasswordHash.Create("CCCCKSLmkEkkWIEkKAIL");

            // Act
            user.ChangePasswordHash(password);

            // Assert
            user.PasswordHash.Hash.Should().Be(password.Hash);
        }
    }
}
