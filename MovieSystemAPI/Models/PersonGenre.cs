using System.ComponentModel.DataAnnotations;

namespace MovieSystemAPI.Models
{
    public class PersonGenre
    {
        [Key]
        public int Id { get; set; }
        public string? Movie { get; set; }
        public int? Rating { get; set; }
        public int PersonId { get; set; }
        public virtual Person Persons { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
