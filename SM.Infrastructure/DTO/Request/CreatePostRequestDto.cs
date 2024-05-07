namespace SM.Infrastructure.DTO.Request;

public class CreatePostRequestDto
{
  public string Message { get; set; }
  public string userId { get; set; }
  public string ParentId { get; set; }
}