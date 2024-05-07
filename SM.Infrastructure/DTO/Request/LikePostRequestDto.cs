namespace SM.Infrastructure.DTO.Request;

public class LikePostRequestDto
{
  public string PostId { get; set; }
  public string UserId { get; set; }
  public bool Liked { get; set; }
}