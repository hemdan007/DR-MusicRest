using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using DR_MusicRest.Models;

namespace TestSongs
{
    public class SongsDatabaseUnitTests
    {
        private static SongsDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SongsDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new SongsDbContext(options);
        }

        [Fact]
        public void GetAll_ReturnsSeededSongs_FromDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            context.Songs.AddRange(
                new Song { Title = "A", Artist = "X", Duration = 100, PublicationYear = 2000 },
                new Song { Title = "B", Artist = "Y", Duration = 120, PublicationYear = 2021 }
            );
            context.SaveChanges();

            var repo = new SongsDatabase(context);

            // Act
            var all = repo.GetAll().ToList();

            // Assert
            Assert.Equal(2, all.Count);
            Assert.Contains(all, s => s.Title == "A");
            Assert.Contains(all, s => s.Title == "B");
        }

        [Fact]
        public void Add_PersistsSongAndAssignsId_InDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var repo = new SongsDatabase(context);
            var song = new Song { Title = "New", Artist = "T", Duration = 90, PublicationYear = 2022 };

            // Act
            var added = repo.Add(song);

            // Assert
            Assert.NotNull(added);
            Assert.True(added.Id > 0, "EF should assign a generated Id on save.");
            var all = repo.GetAll().ToList();
            Assert.Single(all);
            Assert.Equal(added.Id, all[0].Id);
            Assert.Equal("New", all[0].Title);
        }

        [Fact]
        public void GetAll_IgnoresSearchParameter_CurrentImplementation()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            context.Songs.Add(new Song { Title = "MatchMe", Artist = "A", Duration = 1, PublicationYear = 2000 });
            context.Songs.Add(new Song { Title = "Other", Artist = "B", Duration = 1, PublicationYear = 2001 });
            context.SaveChanges();

            var repo = new SongsDatabase(context);

            // Act - pass a search string (current SongsDatabase implementation ignores it)
            var results = repo.GetAll("MatchMe");

            // Assert - current implementation returns all entries
            Assert.Equal(2, results.Count());
        }
    }
}