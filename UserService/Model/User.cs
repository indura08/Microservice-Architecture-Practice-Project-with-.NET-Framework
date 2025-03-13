using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "An email is required")]
        public string EmailAddress { get; set; } = null!;
        [Required(ErrorMessage = "A password is must")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Without a name how can we identify you ? with primary key value?")]
        public string Name { get; set; } = null!; 
    }
}
