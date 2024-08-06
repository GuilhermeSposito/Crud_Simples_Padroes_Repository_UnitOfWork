using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoTeste2.DTO_s;

public class RegisterModel
{
    [Required(ErrorMessage = "UserName is required")] public string? Username { get; set; }
    [EmailAddress][Required(ErrorMessage = "Email is required")] public string? Email { get; set; }
    [Required(ErrorMessage = "PassWord is required")] public string? Password { get; set; }
}
