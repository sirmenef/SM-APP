using Microsoft.EntityFrameworkCore;
using SM.Infrastructure.Model;

namespace SM.Infrastructure.DTO.Response;

public class GetPostsResponseDto
{
  public GetPostsResponseDto(List<Post> posts, int page, int pageSize, int total)
  {
    Posts = Posts;
    PageSize = pageSize;
    Page = page;
    TotalCount = total;
  }
  public List<Post> Posts { get; set; }
  public int Page { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }
  public bool HasNextPage => Page * PageSize < TotalCount;
  public bool HasPrevPage => PageSize > 1;

  public static async Task<GetPostsResponseDto> GetPostsAsync(IQueryable<Post> query, int page, int pageSize)
  {
    var total = await query.CountAsync();
    var posts = await query
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync();

    return new(posts, page, pageSize, total);
  }
}