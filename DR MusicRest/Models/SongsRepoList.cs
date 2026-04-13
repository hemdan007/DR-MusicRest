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

        public List <Song>  GetAll()
        {
            // Return a copy of the list to prevent external modification
            return new List<Song>(_songsRepo);
        }

        public Song Add(Song song)
        {
            song.Id = nextId++;
            _songsRepo.Add(song);
            return song;
        }
    }
}
