using ClientServer.Requests.AlbumsRequests;
using FluentValidation;

namespace ClientServer.Validators.AlbumsRequestsValidators;

public class GetAlbumValidator : AbstractValidator<GetAlbumRequest>
{
    public GetAlbumValidator()
    {
        RuleFor(request => request)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Model is empty");
        RuleFor(request => request.Id)
            .Cascade(CascadeMode.Stop)
            .Must(x => x.ToString().Length == 36)
            .WithMessage("Guid is incorrect");
    }
}