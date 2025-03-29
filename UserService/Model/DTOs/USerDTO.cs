using System.ComponentModel.DataAnnotations;

namespace UserService.Model.DTOs
{
    public class USerDTO
    {
        [Required(ErrorMessage = "Without a name how can we identify you ? with primary key value?")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "An email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "A password is must")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
