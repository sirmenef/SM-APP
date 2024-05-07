using FluentValidation;
using SM.Infrastructure.DTO.Request;

namespace SM.Validations;

public class CreatePostRequestValidator :  AbstractValidator<CreatePostRequestDto> 
{
  public CreatePostRequestValidator()
  {
    RuleFor(x => x.Message)
      .NotEmpty().WithMessage("Message is required.")
      .Length(1, 140).WithMessage("Message must be between 1 and 140 characters.");
  }
}