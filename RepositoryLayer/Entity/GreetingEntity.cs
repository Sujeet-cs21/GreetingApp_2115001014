using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Entity
{
    public class GreetingEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Greeting { get; set; }
    }
}
