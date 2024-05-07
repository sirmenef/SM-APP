using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SM.Infrastructure.DTO.Request;
using SM.Infrastructure.DTO.Response;
using SM.Infrastructure.EF;
using SM.Infrastructure.Model;

namespace SM.Core.Services;

public class PostsService
{
  public AppDbContext Context { get; }

  public PostsService(AppDbContext context, UserManager<User> userManager)
  {
    Context = context;
  }

  public async Task<Post> CreatePost(CreatePostRequestDto dto)
  {
    var post = new Post
    {
      UserId = dto.userId,
      PostParentId = dto.ParentId,
      Message = dto.Message,
    };

    Context.Posts.Add(post);
    await Context.SaveChangesAsync();
    return post;
  }

  public async Task<Post?> GetPost(string id)
  {
    return await Context.Posts.FirstOrDefaultAsync(post => post.PostId == id);
  }

  public async Task<GetPostsResponseDto> GetAllPosts(GetPostsRequestDto dto)
  {
    var friends = Context.Friends
      .Where(x => x.UserId.Equals(dto.UserId))
      .Select(x => x.FriendId);

    var posts = Context.Posts
      .Where(p => friends.Any(fId => fId.Equals(p.UserId)))
      .OrderByDescending(p => p.Likes);
    
    return await GetPostsResponseDto.GetPostsAsync(posts, dto.Page, dto.PageSize);
  }

  public async Task LikePost(LikePostRequestDto dto)
  {
    var post = await GetPost(dto.PostId);
    if (post is null)
    {
      throw new Exception("Post not found");
      
    }
    var likes = await Context.Likes
      .SingleOrDefaultAsync(x => x.PostId.Equals(dto.PostId) && x.userId.Equals(dto.UserId));

    if (likes is not null)
    {
      if (likes is not null)
      {
        likes.Liked = dto.Liked;
        likes.UpdatedOn = DateTime.UtcNow;
        await Context.SaveChangesAsync();
      }
      else
      {
        throw new ArgumentException("User can not perform operation");
      }
    }
    else
    {
      var like = new Likes
        { Id = Guid.NewGuid().ToString(), Liked = dto.Liked, userId = dto.UserId, PostId = dto.PostId };
      Context.Likes.Add(like);
      await Context.SaveChangesAsync();
    }
  }
}