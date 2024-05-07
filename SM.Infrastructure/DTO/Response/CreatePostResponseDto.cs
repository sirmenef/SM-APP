namespace SM.Infrastructure.DTO.Response;

public class CreatePostResponseDto
{
  public Guid PostId { get; set; }
  public string Message { get; set; }
  public DateTime CreatedOn { get; set; }
}