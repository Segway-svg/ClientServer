using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using ClientServer.Validators.AlbumsRequestsValidators;
using MassTransit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ClientServer.Commands.AlbumsCommands;

public class UpdateAlbumCommand
{
    public UpdateAlbumValidator updateAlbumValidator = new UpdateAlbumValidator();

    public async Task<UpdateAlbumResponse> Execute(IRequestClient<UpdateAlbumRequest> request, UpdateAlbumRequest updateAlbumRequest)
    {
        if (updateAlbumRequest == null)
        {
            var message = "Request is empty";
            var failureResponse = new UpdateAlbumResponse()
            {
                StatusCode = false,
                Errors = new List<string>()
            };
            failureResponse.Errors.Add(message);
            return failureResponse;
        }

        ValidationResult validationResult = updateAlbumValidator.Validate(updateAlbumRequest);

        if (!validationResult.IsValid)
        {
            var failureResponse = new UpdateAlbumResponse()
            {
                StatusCode = false,
                Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList()
            };
            return failureResponse;
        }

        var response = await request.GetResponse<UpdateAlbumResponse>(updateAlbumRequest);
        return response.Message;
    }
}