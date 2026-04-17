using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using DR_MusicRest.Controllers;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsControllerDeleteTests
    {
        [Fact]
        public void Delete_ReturnsOkAndRemovesSong_WhenExists()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);
            var existing = repo.GetAll().First();
            var id = existing.Id;

            // Act
            var action = controller.Delete(id);

            // Assert - controller returns OkObjectResult with a message
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            var message = Assert.IsType<string>(ok.Value);
            Assert.Contains("has been deleted", message);
            Assert.Contains(existing.Title, message);

            // Verify repo no longer contains the song
            Assert.Null(repo.GetSongById(id));
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);

            // Act
            var action = controller.Delete(99999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(action.Result);
        }
    }
}