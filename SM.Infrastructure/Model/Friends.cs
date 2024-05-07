using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SM.Infrastructure.Model;

[Table("Friends")]
public class Friends
{
  public string Id { get; set; }
  public string FriendId { get; set; }
  public User Friend { get; set; }
  public string UserId { get; set; }
  public User User { get; set; }
}