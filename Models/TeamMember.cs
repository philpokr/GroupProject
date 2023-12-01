namespace GroupProject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeamMember
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? CollegeProgram { get; set; }
        public string? YearInProgram { get; set; }
    }
}
