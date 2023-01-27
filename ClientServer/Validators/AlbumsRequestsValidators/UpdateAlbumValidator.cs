using ClientServer.Requests.AlbumsRequests;
using FluentValidation;

namespace ClientServer.Validators.AlbumsRequestsValidators;

public class UpdateAlbumValidator : AbstractValidator<UpdateAlbumRequest>
{
    public UpdateAlbumValidator()
    {
        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Model is empty");
        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .Must(x => x.ToString().Length == 36)
            .WithMessage("Guid is incorrect");
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