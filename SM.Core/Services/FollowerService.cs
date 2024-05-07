using SM.Infrastructure.DTO.Request;
using SM.Infrastructure.EF;
using SM.Infrastructure.Model;

namespace SM.Core.Services;

public class FollowerService
{
  private readonly AppDbContext _context;

  public FollowerService(AppDbContext context)
  {
    _context = context;
  }

  public async Task Follow(FollowRequestDto dto)
  {
    var user = _context.Users.FirstOrDefault(x => x.Id.Equals(dto.UserId));
    var follower = _context.Users.SingleOrDefault(x => x.Id.Equals(dto.FollowerId));
    var follow = _context.Friends.SingleOrDefault(x => x.UserId == dto.UserId && x.FriendId == dto.FollowerId);

    if (follower != null && follow == null)
    {
      _context.Friends.Add(new Friends
      {
        UserId = user.Id, FriendId = follower.Id
      });
      await _context.SaveChangesAsync();
    }
  }
  
  public async Task UnFollow(UnFollowRequestDto dto)
  {
    var follow = _context.Friends.SingleOrDefault(x => x.UserId == dto.UserId && x.FriendId == dto.FollowerId);
    if (follow != null )
    {
      _context.Friends.Remove(follow);
      await _context.SaveChangesAsync();
    }
  }
}