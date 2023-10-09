using System.ComponentModel.DataAnnotations;

namespace UniApi.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Sex { get; set; }

    }
}
