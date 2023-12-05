using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models
{
    public class FavoriteSport
    {
        [Key]

        public string? SportTitle { get; set; }
        public string? FieldsOfPlay { get; set; }
        public int NumOfPlayers { get; set; }
        public DateTime? LastTimeWatchedOrPlayed { get; set; }
    }
}
