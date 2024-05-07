using AutoMapper;
using SM.Infrastructure.DTO.Response;
using SM.Infrastructure.Model;

namespace SM.Mappings;

public class PostProfile : Profile
{
  public PostProfile()
  {
    CreateMap<Post, CreatePostResponseDto>();
    CreateMap<Post, GetPostsResponseDto>();
  }
}

