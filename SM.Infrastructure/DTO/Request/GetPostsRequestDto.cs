using SM.Infrastructure.Model;

namespace SM.Infrastructure.DTO.Request;

public class GetPostsRequestDto
{
  public string UserId { get; set; }
  public int Page { get; set; }
  public int PageSize { get; set; }
}