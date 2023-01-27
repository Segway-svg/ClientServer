using ClientServer.Requests.AlbumsRequests;
using FluentValidation;

namespace ClientServer.Validators.AlbumsRequestsValidators.Create;

public class CreateAlbumValidator : AbstractValidator<CreateAlbumRequest>, ICreateAlbumValidator
{
    public CreateAlbumValidator()
    {
        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is empty")
            .MinimumLength(5)
            .WithMessage("Name is too short")
            .MaximumLength(20)
            .WithMessage("Name is too long");
        RuleFor(request => request.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is empty")
            .MinimumLength(10)
            .WithMessage("Description is too short")
            .MaximumLength(32)
            .WithMessage("Description is too long");
    }
}