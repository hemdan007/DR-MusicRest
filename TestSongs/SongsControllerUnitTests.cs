using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using DR_MusicRest.Controllers;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsControllerUnitTests
    {
        [Fact]
        public void GetById_ReturnsSong_WhenExists()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);
            var existing = repo.GetAll().First();
            var id = existing.Id;

            // Act
            var action = controller.Get(id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            var returned = Assert.IsType<Song>(ok.Value);
            Assert.Equal(id, returned.Id);
            Assert.Equal(existing.Title, returned.Title);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);

            // Act
            var action = controller.Get(9999);

            // Assert
            Assert.IsType<NotFoundObjectResult>(action.Result);
        }

        [Fact]
        public void Put_UpdatesSong_WhenExists()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);
            var existing = repo.GetAll().First();
            var updated = new Song
            {
                Title = "Updated Title",
                Artist = "Updated Artist",
                Duration = existing.Duration + 10,
                PublicationYear = existing.PublicationYear + 1
            };

            // Act
            var action = controller.Put(existing.Id, updated);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(action.Result);
            var returned = Assert.IsType<Song>(ok.Value);
            Assert.Equal(existing.Id, returned.Id);
            Assert.Equal("Updated Title", returned.Title);
            Assert.Equal("Updated Artist", returned.Artist);

            // Verify persisted in repo implementation
            var fromRepo = repo.GetSongById(existing.Id);
            Assert.NotNull(fromRepo);
            Assert.Equal("Updated Title", fromRepo!.Title);
        }

        [Fact]
        public void Put_ReturnsNotFound_WhenMissing()
        {
            // Arrange
            var repo = new SongsRepoList();
            var controller = new SongsController(repo);
            var updated = new Song { Title = "X", Artist = "Y", Duration = 1, PublicationYear = 2000 };

            // Act
            var action = controller.Put(9999, updated);

            // Assert
            Assert.IsType<NotFoundObjectResult>(action.Result);
        }
    }
}