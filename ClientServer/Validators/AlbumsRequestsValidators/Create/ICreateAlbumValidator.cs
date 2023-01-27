using ClientServer.Requests.AlbumsRequests;
using FluentValidation;

namespace ClientServer.Validators.AlbumsRequestsValidators.Create;

public interface ICreateAlbumValidator : IValidator<CreateAlbumRequest>
{
    
}