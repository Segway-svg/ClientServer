using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using ClientServer.Validators.AlbumsRequestsValidators;
using MassTransit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ClientServer.Commands.AlbumsCommands;

public class DeleteAlbumCommand
{
    public DeleteAlbumValidator deleteAlbumValidator = new DeleteAlbumValidator();

    public async Task<DeleteAlbumResponse> Execute(IRequestClient<DeleteAlbumRequest> request, DeleteAlbumRequest deleteAlbumRequest)
    {
        if (deleteAlbumRequest == null)
        {
            var message = "Request is empty";
            var failureResponse = new DeleteAlbumResponse()
            {
                StatusCode = false,
                Errors = new List<string>()
            };
            failureResponse.Errors.Add(message);
            return failureResponse;
        }

        ValidationResult validationResult = deleteAlbumValidator.Validate(deleteAlbumRequest);

        if (!validationResult.IsValid)
        {
            var failureResponse = new DeleteAlbumResponse()
            {
                StatusCode = false,
                Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList()
            };
            return failureResponse;
        }

        var response = await request.GetResponse<DeleteAlbumResponse>(deleteAlbumRequest);
        return response.Message;
    }
}