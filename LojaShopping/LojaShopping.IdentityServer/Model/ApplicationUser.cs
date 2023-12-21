using Microsoft.AspNetCore.Identity;

namespace LojaShopping.IdentityServer.Model;

public class ApplicationUser : IdentityUser

{
    public string PrimeiroNome {  get; set; }
    public string UltimoNome {  get; set; }




}
