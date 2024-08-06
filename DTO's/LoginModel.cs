using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoTeste2.DTO_s;

public class LoginModel
{
 [Required(ErrorMessage = "UserName is required")]  public string? UserName { get; set; }
 [Required(ErrorMessage = "PassWord is required")]  public string? PassWord { get; set; }
}
