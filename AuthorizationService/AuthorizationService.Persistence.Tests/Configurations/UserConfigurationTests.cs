using AuthorizationService.Domain.Entities;
using AuthorizationService.Persistence.Configurations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit;

namespace AuthorizationService.Persistence.Tests.Configurations
{
    public class UserConfigurationTests
    {
        private readonly ModelBuilder _builder = new();
        private readonly UserConfiguration _configuration = new();
        private readonly IEntityType _user;

        public UserConfigurationTests()
        {
            _configuration.Configure(_builder.Entity<User>());
            _user = _builder.FinalizeModel().FindEntityType(typeof(User))!;
        }

        [Fact]
        public void Configure_ShouldSetTableNameToUsers()
        {
            // Act
            var tableName = _user.GetTableName();

            // Assert
            tableName.Should().Be("users");
        }
    }
}
