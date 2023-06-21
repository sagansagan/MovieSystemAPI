using System.ComponentModel.DataAnnotations;

namespace MovieSystemAPI.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [MaxLength(30)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Email { get; set; }
        public List<PersonGenre> PersonGenres { get; set; }
    }
}
