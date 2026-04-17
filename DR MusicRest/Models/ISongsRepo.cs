namespace DR_MusicRest.Models
{
    public interface ISongsRepo
    {
        IEnumerable<Song> GetAll(string? search = null);

        Song Add(Song song);
        Song? GetSongById(int id);
        Song? DeleteSong(int id);
        Song? UpdateSong(int id, Song updatedSong);
    }
}