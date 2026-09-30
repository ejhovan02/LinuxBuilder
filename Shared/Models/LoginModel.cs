using System.ComponentModel.DataAnnotations;

namespace LinuxBuilder.Shared.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}