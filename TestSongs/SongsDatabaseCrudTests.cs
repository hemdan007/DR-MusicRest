using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsDatabaseCrudTests
    {
        private static SongsDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SongsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new SongsDbContext(options);
        }

        [Fact]
        public void GetSongById_ReturnsSong_WhenExists()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var song = new Song { Title = "Hello", Artist = "A", Duration = 200, PublicationYear = 2020 };
            context.Songs.Add(song);
            context.SaveChanges();

            var repo = new SongsDatabase(context);

            // Act
            var found = repo.GetSongById(song.Id);

            // Assert
            Assert.NotNull(found);
            Assert.Equal(song.Id, found!.Id);
            Assert.Equal("Hello", found.Title);
            Assert.Equal("A", found.Artist);
        }

        [Fact]
        public void GetSongById_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var repo = new SongsDatabase(context);

            // Act
            var found = repo.GetSongById(12345);

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void UpdateSong_UpdatesAndPersists_WhenExists()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var original = new Song { Title = "Old", Artist = "X", Duration = 100, PublicationYear = 2000 };
            context.Songs.Add(original);
            context.SaveChanges();

            var repo = new SongsDatabase(context);
            var updatedData = new Song { Title = "New", Artist = "Y", Duration = 150, PublicationYear = 2022 };

            // Act
            var result = repo.UpdateSong(original.Id, updatedData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(original.Id, result!.Id);
            Assert.Equal("New", result.Title);
            Assert.Equal("Y", result.Artist);
            Assert.Equal(150, result.Duration);
            Assert.Equal(2022, result.PublicationYear);

            // Verify persisted to the DbContext
            var reloaded = context.Songs.Find(original.Id);
            Assert.NotNull(reloaded);
            Assert.Equal("New", reloaded!.Title);
        }

        [Fact]
        public void UpdateSong_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var repo = new SongsDatabase(context);
            var updated = new Song { Title = "New", Artist = "Y", Duration = 150, PublicationYear = 2022 };

            // Act
            var result = repo.UpdateSong(9999, updated);

            // Assert
            Assert.Null(result);
        }
    }
}