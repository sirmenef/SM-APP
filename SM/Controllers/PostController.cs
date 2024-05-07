using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Services;
using SM.Infrastructure.DTO;
using SM.Infrastructure.DTO.Request;
using SM.Infrastructure.DTO.Response;
using SM.Infrastructure.Model;
using SM.Validations;

namespace SM.Controllers;

[Route("api/[controller]")]
// add fluent validation
public class PostController : Controller
{
  private readonly PostsService _postsService;
  private readonly IMapper _mapper;
  private readonly User currentUser;

  public PostController(PostsService postsService, IMapper mapper, UserManager<User> user)
  {
    _postsService = postsService;
    _mapper = mapper;
    currentUser = user.Users.FirstOrDefault();
  }
  
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id)
  {
    try
    {
      var post = await _postsService.GetPost(id);
      if (post == null)
      {
        return NotFound();
      }
      return Ok(post);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }
  
  [HttpPost]
  [Authorize]
  public async Task<IActionResult> CreatePost([FromBody]CreatePostRequestDto dto)
  {
    var validator = new CreatePostRequestValidator();
    var isValid = await validator.ValidateAsync(dto);
    if (!isValid.IsValid)
    {
      return BadRequest(isValid.ToString(", "));
    }
    
    try
    {
      dto.userId = currentUser.Id;
      var post = await _postsService.CreatePost(dto);
      var result = _mapper.Map<CreatePostResponseDto>(post);
      return Ok(result);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpGet("")]
  public async Task<IActionResult> GetAllPosts([FromBody] GetPostsRequestDto dto)
  {
    try
    {
      dto.UserId = currentUser.Id;
      var posts = await _postsService.GetAllPosts(dto);

      if (posts == null || !posts.Posts.Any())
      {
        return NotFound("No posts found");
      }

      return Ok(posts);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpPost("like")]
  public async Task<IActionResult> LikePost([FromBody] LikePostRequestDto dto)
  {
    dto.UserId = currentUser.Id;

    try
    {
      await _postsService.LikePost(dto);

      return Ok();
    }
    catch (ArgumentException ex)
    {
      return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status404NotFound, e.Message);
    }
  }
  
}