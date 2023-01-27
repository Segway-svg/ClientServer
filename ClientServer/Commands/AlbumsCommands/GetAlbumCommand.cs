using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using ClientServer.Validators.AlbumsRequestsValidators;
using MassTransit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ClientServer.Commands.AlbumsCommands;

public class GetAlbumCommand
{
    public GetAlbumValidator getAlbumValidator = new GetAlbumValidator();

    public async Task<GetAlbumResponse> Execute(IRequestClient<GetAlbumRequest> request, GetAlbumRequest getAlbumRequest)
    {
        if (getAlbumRequest == null)
        {
            var message = "Request is empty";
            var failureResponse = new GetAlbumResponse()
            {
                StatusCode = false,
                Errors = new List<string>()
            };
            failureResponse.Errors.Add(message);
            return failureResponse;
        }

        ValidationResult validationResult = getAlbumValidator.Validate(getAlbumRequest);

        if (!validationResult.IsValid)
        {
            var failureResponse = new GetAlbumResponse()
            {
                StatusCode = false,
                Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList()
            };
            return failureResponse;
        }

        var response = await request.GetResponse<GetAlbumResponse>(getAlbumRequest);
        return response.Message;
    }
}