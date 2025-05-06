using System.ComponentModel.DataAnnotations;

namespace api.ViewModels;

public class RegisterUserViewModel : LoginViewModel
{
  [Required]
  [StringLength(15, ErrorMessage = "Nu gjorde du allt bort dig!")]
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public string ConfirmPassword { get; set; } = "";
}
