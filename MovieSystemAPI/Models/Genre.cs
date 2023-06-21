using System.ComponentModel.DataAnnotations;

namespace MovieSystemAPI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        public List<PersonGenre> PersonGenres { get; set; }
    }
}
