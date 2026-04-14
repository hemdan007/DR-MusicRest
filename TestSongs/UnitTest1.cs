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
            var result = repo.GetAll().ToList();

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
            var initialCount = repo.GetAll().ToList().Count;
            var returnedList = repo.GetAll().ToList();

            // Act - modify the returned list
            returnedList.RemoveAt(0);

            // Assert - repository should still return the original count (GetAll returns a copy)
            Assert.Equal(initialCount, repo.GetAll().ToList().Count);
            // And each call to GetAll returns a different List instance
            Assert.NotSame(returnedList, repo.GetAll());
        }

        [Fact]
        public void Add_IncreasesCountAndAssignsId()
        {
            // Arrange
            var repo = new SongsRepoList();
            var before = repo.GetAll().ToList().Count;
            var newSong = new Song
            {
                // Id intentionally left at default (0) - repo should assign
                Title = "New Track",
                Artist = "Test Artist",
                Duration = 123,
                PublicationYear = 2025
            };

            // Act
            var added = repo.Add(newSong);
            var after = repo.GetAll();

            // Assert
            Assert.NotNull(added);
            Assert.True(added.Id > 0, "Expected assigned Id to be > 0.");
            Assert.Equal(before + 1, after.ToList().Count);
            Assert.Contains(after, s => s.Id == added.Id && s.Title == "New Track");
        }

        [Fact]
        public void Add_ReturnsSameInstanceWithAssignedId()
        {
            // Arrange
            var repo = new SongsRepoList();
            var song = new Song { Title = "Instance Test", Artist = "A", Duration = 100, PublicationYear = 2000 };

            // Act
            var returned = repo.Add(song);

            // Assert
            // The repository sets the Id on the instance and returns it
            Assert.Same(song, returned);
            Assert.True(returned.Id > 0);
            Assert.Equal(song.Id, returned.Id);
        }

        [Fact]
        public void Add_AssignsUniqueIncrementingIds_OnMultipleAdds()
        {
            // Arrange
            var repo = new SongsRepoList();
            var s1 = repo.Add(new Song { Title = "S1", Artist = "X", Duration = 10, PublicationYear = 2001 });
            var s2 = repo.Add(new Song { Title = "S2", Artist = "Y", Duration = 20, PublicationYear = 2002 });

            // Act & Assert
            Assert.NotEqual(s1.Id, s2.Id);
            Assert.True(s2.Id > s1.Id);
        }

        [Fact]
        public void GetAll_FilterByPublicationYear_ReturnsOnlyRecentSongs()
        {
            // Arrange
            var repo = new SongsRepoList();

            // Act - filter client-side using LINQ
            var recent = repo.GetAll().Where(s => s.PublicationYear >= 2020).ToList();

            // Assert
            // From seeded data: "Baby" (2020) and "Darling" (2021) -> 2 songs
            Assert.Equal(2, recent.Count);
            Assert.All(recent, s => Assert.True(s.PublicationYear >= 2020));
            // Ensure repository data unchanged
            Assert.Equal(4, repo.GetAll().ToList().Count);
        }

        [Fact]
        public void GetAll_FilterByArtist_ReturnsMatchesWithoutChangingRepo()
        {
            // Arrange
            var repo = new SongsRepoList();

            // Act - filter client-side by exact artist
            var mjSongs = repo.GetAll().Where(s => s.Artist == "M J").ToList();

            // Assert
            Assert.Single(mjSongs);
            Assert.Equal("Dirty Diana", mjSongs[0].Title);
            // Ensure repository data unchanged
            Assert.Equal(4, repo.GetAll().ToList().Count);
        }
    }
}
