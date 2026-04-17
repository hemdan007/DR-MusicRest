using System.Linq;
using Xunit;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsRepoListDeleteTests
    {
        [Fact]
        public void DeleteSong_RemovesAndReturnsSong_WhenExists()
        {
            // Arrange
            var repo = new SongsRepoList();
            var before = repo.GetAll().ToList().Count;
            var existing = repo.GetAll().First();
            var id = existing.Id;

            // Act
            var deleted = repo.DeleteSong(id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(id, deleted!.Id);
            Assert.Equal(before - 1, repo.GetAll().ToList().Count);
            Assert.Null(repo.GetSongById(id));
        }

        [Fact]
        public void DeleteSong_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var repo = new SongsRepoList();

            // Act
            var deleted = repo.DeleteSong(99999);

            // Assert
            Assert.Null(deleted);
        }
    }
}