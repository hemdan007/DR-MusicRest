namespace DR_MusicRest.Models
{
    public class SongsRepoList : ISongsRepo
    {
        public List<Song> _songsRepo = new List<Song>();
        private int nextId = 1;
        public SongsRepoList()
        {
            _songsRepo.Add(new Song { Id = nextId++, Title = "Baby", Artist = "J B", Duration = 210, PublicationYear = 2020 });
            _songsRepo.Add(new Song { Id = nextId++, Title = "Skat", Artist = "Dig", Duration = 180, PublicationYear = 2019 });
            _songsRepo.Add(new Song { Id = nextId++, Title = "Darling", Artist = "Mig", Duration = 240, PublicationYear = 2021 });
            _songsRepo.Add(new Song { Id = nextId++, Title = "Dirty Diana", Artist = "M J", Duration = 200, PublicationYear = 1999 });
        }

        public IEnumerable<Song> GetAll(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<Song>(_songsRepo);
            }

            return _songsRepo
                .Where(s =>
                    s.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.Artist.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Song? GetSongById(int id)
        {
            return _songsRepo.FirstOrDefault(m => m.Id == id);
        }


        public Song Add(Song song)
        {
            song.Id = nextId++;
            _songsRepo.Add(song);
            return song;
        }



        public Song? UpdateSong(int id, Song updatedSong)
        {
            var existingSong = GetSongById(id);
            if (existingSong != null)
            {
                existingSong.Title = updatedSong.Title;
                existingSong.Artist = updatedSong.Artist;
                existingSong.Duration = updatedSong.Duration;
                existingSong.PublicationYear = updatedSong.PublicationYear;
                
                return existingSong;
            }
            return null;
        }


        // Method to delete an existing song by its ID
        public Song? DeleteSong(int id)
        {
            var song = GetSongById(id);

            if (song != null)
            {
                _songsRepo.Remove(song);
                return song;
            }

            return null;
        }


    }
}
