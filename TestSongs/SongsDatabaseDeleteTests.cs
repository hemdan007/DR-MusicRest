using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsDatabaseDeleteTests
    {
        private static SongsDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SongsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new SongsDbContext(options);
        }

        [Fact]
        public void DeleteSong_RemovesAndReturnsSong_WhenExists()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var song = new Song { Title = "ToDelete", Artist = "X", Duration = 10, PublicationYear = 2000 };
            context.Songs.Add(song);
            context.SaveChanges();

            var repo = new SongsDatabase(context);

            // Act
            var deleted = repo.DeleteSong(song.Id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(song.Id, deleted!.Id);
            Assert.Null(context.Songs.Find(song.Id));
        }

        [Fact]
        public void DeleteSong_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var repo = new SongsDatabase(context);

            // Act
            var deleted = repo.DeleteSong(99999);

            // Assert
            Assert.Null(deleted);
        }
    }
}