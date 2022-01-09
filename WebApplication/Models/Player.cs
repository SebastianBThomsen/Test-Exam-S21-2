using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Range(1,99)]
        public int ShirtNumber { get; set; }
        public decimal Salary { get; set; }
        public int GoalsThisSeason { get; set; }
        [Required]
        public string Position { get; set; }
    }
}