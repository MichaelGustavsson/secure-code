using System.ComponentModel.DataAnnotations;

namespace secure_api.ViewModels;

public class UserLoginViewModel
{
    [Required]
    [MaxLength(80)]
    [EmailAddress]
    public required string UserName { get; set; }

    [Required]
    [MaxLength(8)]
    public required string Password { get; set; }
}
