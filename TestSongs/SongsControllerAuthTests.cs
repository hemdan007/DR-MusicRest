using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Microsoft.AspNetCore.Authorization;
using DR_MusicRest.Controllers;

namespace TestSongs
{
    public class SongsControllerAuthTests
    {
        private static MethodInfo FindMethod(Type type, string name, params Type[] parameterTypes)
        {
            return type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                       .FirstOrDefault(m => m.Name == name
                                            && m.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes))
                   ?? throw new InvalidOperationException($"Method '{name}' with specified signature not found on type '{type.FullName}'.");
        }

        private static AuthorizeAttribute GetAuthorizeAttribute(MemberInfo member)
        {
            return member.GetCustomAttribute<AuthorizeAttribute>(inherit: false);
        }

        [Fact]
        public void Get_WithSearch_IsProtectedForAdminAndUser()
        {
            // Arrange
            var type = typeof(SongsController);
            var method = FindMethod(type, "Get", typeof(string));

            // Act
            var attr = GetAuthorizeAttribute(method);

            // Assert
            Assert.NotNull(attr);
            Assert.Equal("admin, user", attr.Roles);
        }

        [Fact]
        public void Get_ById_IsProtectedForAdminAndUser()
        {
            // Arrange
            var type = typeof(SongsController);
            var method = FindMethod(type, "Get", typeof(int));

            // Act
            var attr = GetAuthorizeAttribute(method);

            // Assert
            Assert.NotNull(attr);
            Assert.Equal("admin, user", attr.Roles);
        }

        [Fact]
        public void Post_IsRestrictedToAdmin()
        {
            // Arrange
            var type = typeof(SongsController);
            var method = FindMethod(type, "Post", typeof(DR_MusicRest.Models.Song));

            // Act
            var attr = GetAuthorizeAttribute(method);

            // Assert
            Assert.NotNull(attr);
            Assert.Equal("admin", attr.Roles);
        }

        [Fact]
        public void Put_IsRestrictedToAdmin()
        {
            // Arrange
            var type = typeof(SongsController);
            var method = FindMethod(type, "Put", typeof(int), typeof(string));

            // Act
            var attr = GetAuthorizeAttribute(method);

            // Assert
            Assert.NotNull(attr);
            Assert.Equal("admin", attr.Roles);
        }

        [Fact]
        public void Delete_IsRestrictedToAdmin()
        {
            // Arrange
            var type = typeof(SongsController);
            var method = FindMethod(type, "Delete", typeof(int));

            // Act
            var attr = GetAuthorizeAttribute(method);

            // Assert
            Assert.NotNull(attr);
            Assert.Equal("admin", attr.Roles);
        }
    }
}