using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Services;
using SM.Infrastructure.DTO.Request;
using SM.Infrastructure.Model;

namespace SM.Controllers;

[Route("api/[controller]"), Authorize]
public class FollowerController : Controller
{
  private readonly FollowerService _service;
  private readonly User _currentUser;

  public FollowerController(FollowerService service, UserManager<User> user)
  {
    _service = service;
    _currentUser = user.Users.FirstOrDefault();
  }

  [HttpPost, Authorize]
  public async Task<IActionResult> Follow([FromBody]FollowRequestDto dto)
  {
    try
    {
      dto.UserId = _currentUser.Id;
      await _service.Follow(dto);

      return Ok();
    }
    catch (Exception ex)
    {
      // Log the error
      return StatusCode(500, "Internal server error");
    }
  }


  [HttpPost("unfollow"), Authorize]
  public async Task<IActionResult> Unfollow([FromBody] UnFollowRequestDto dto)
  {
    try
    {
      dto.UserId = _currentUser.Id;
      await _service.UnFollow(dto);
      return Ok();
    }
    catch (Exception ex)
    {
      // Log the error
      return StatusCode(500, "Internal server error");
    }
  }
}