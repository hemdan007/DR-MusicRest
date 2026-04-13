namespace DR_MusicRest.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; } // Duration in seconds
        public int PublicationYear { get; set; }

        public override string ToString()
        {
            return $"{Title} by {Artist}, Duration: {Duration} seconds, Published: {PublicationYear}, and it´s Id: {Id}";
        }
    }  
}
