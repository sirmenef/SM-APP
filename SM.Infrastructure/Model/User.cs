using Microsoft.AspNetCore.Identity;
namespace SM.Infrastructure.Model;

public class User : IdentityUser
{
  public ICollection<Post> Posts { get; set; }
  public ICollection<Friends> Followers { get; set; }
  public ICollection<Friends> Following { get; set; }
}