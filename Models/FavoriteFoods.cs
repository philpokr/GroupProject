using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models
{
    public class FavoriteFoods
    {
        [Key]
        public int FoodId { get; set; }

        public string? FoodName { get; set; }
        public string? Cuisine { get; set; }
        public decimal Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime? LastTasted { get; set; }
    }
}
