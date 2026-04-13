namespace DR_MusicRest.Models
{
    public interface ISongsRepo
    {
        List<Song> GetAll();
        Song Add(Song song);
        Song? GetSongById(int id);
        Song? RemoveSong(int id);
        Song? UpdateSong(int id, Song updatedSong);
    }
}