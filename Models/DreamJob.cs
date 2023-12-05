using System.ComponentModel.DataAnnotations;

namespace GroupProject.Models
{
    public class DreamJob
    {
        [Key]
        public int Salary { get; set; }

        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }
        public decimal Rating { get; set; }
        public string? Benefits { get; set; }
        public decimal? PrefferedHours { get; set; }
    }
}
