using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SM.Infrastructure.Model;

[Table("Posts")]
public class Post
{
  public string PostId { get; set; }
  public string UserId { get; set; }
  public User User { get; set; }
  public string? PostParentId { get; set; }
  public DateTime CreatedOn { get; set; }
  public DateTime? UpdatedOn { get; set; }
  public string Message { get; set; }
  public ICollection<Likes> Likes { get; set; }
  public bool? IsDeleted { get; set; }
  public DateTime? DeletedOn { get; set; }
}

[Table("Likes")]
public class Likes
{
  public string Id { get; set; }
  public bool Liked { get; set; }
  public string userId { get; set; }
  public string PostId { get; set; }
  public Post Post { get; set; }
  public DateTime CreatedOn { get; set; }
  public DateTime UpdatedOn { get; set; }
}