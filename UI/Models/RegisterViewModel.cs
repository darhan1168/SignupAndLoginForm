using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Firstname is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Lastname is required")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email address is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}