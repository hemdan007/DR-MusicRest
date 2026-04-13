using System.Linq;
using Xunit;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsRepoListTests
    {
        [Fact]
        public void GetAll_ReturnsInitialSongs()
        {
            // Arrange
            var repo = new SongsRepoList();

            // Act
            var result = repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Count); // constructor seeds 4 songs
            Assert.True(result.Any(s => s.Title == "Baby"), "Expected a song with title 'Baby' to be present.");
            Assert.True(result.Any(s => s.Title == "Dirty Diana"), "Expected a song with title 'Dirty Diana' to be present.");
        }

        [Fact]
        public void GetAll_ReturnsASeparateList_WhenModified()
        {
            // Arrange
            var repo = new SongsRepoList();
            var initialCount = repo.GetAll().Count;
            var returnedList = repo.GetAll();

            // Act - modify the returned list
            returnedList.RemoveAt(0);

            // Assert - repository should still return the original count (GetAll returns a copy)
            Assert.Equal(initialCount, repo.GetAll().Count);
            // And each call to GetAll returns a different List instance
            Assert.NotSame(returnedList, repo.GetAll());
        }
    }
}
