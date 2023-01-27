using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using ClientServer.Validators.AlbumsRequestsValidators.Create;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ClientServer.Commands.AlbumsCommands.Create;


public class CreateAlbumCommand : ICreateAlbumCommand
{
    private IRequestClient<CreateAlbumRequest> _request;
    private ICreateAlbumValidator _createAlbumValidator;

    public CreateAlbumCommand(ICreateAlbumValidator createAlbumValidator, IRequestClient<CreateAlbumRequest> request)
    {
        _createAlbumValidator = createAlbumValidator;
        _request = request;
    }

    public async Task<CreateAlbumResponse> Execute(CreateAlbumRequest createAlbumRequest)
    {
        if (createAlbumRequest == null)
        {
            var message = "Request is empty";
            var failureResponse = new CreateAlbumResponse()
            {
                StatusCode = false,
                Errors = new List<string>()
            };
            failureResponse.Errors.Add(message);
            return failureResponse;
        }

        ValidationResult validationResult = await _createAlbumValidator.ValidateAsync(createAlbumRequest);

        if (!validationResult.IsValid)
        {
            var failureResponse = new CreateAlbumResponse()
            {
                StatusCode = false,
                Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList()
            };
            return failureResponse;
        }
        
        var response = await _request.GetResponse<CreateAlbumResponse>(createAlbumRequest);
        return response.Message;
    }
}

// using ClientServer.Requests.AlbumsRequests;
// using ClientServer.Responses.AlbumsResponses;
// using ClientServer.Validators.AlbumsRequestsValidators.Create;
// using MassTransit;
// using ValidationResult = FluentValidation.Results.ValidationResult;
//
// namespace ClientServer.Commands.AlbumsCommands.Create;
//
//
// public class CreateAlbumCommand : ICreateAlbumCommand
// {
//     public CreateAlbumValidator createAlbumValidator = new CreateAlbumValidator();
//
//     public async Task<CreateAlbumResponse> Execute(IRequestClient<CreateAlbumRequest> request, CreateAlbumRequest createAlbumRequest)
//     {
//         if (createAlbumRequest == null)
//         {
//             var message = "Request is empty";
//             var failureResponse = new CreateAlbumResponse()
//             {
//                 StatusCode = false,
//                 Errors = new List<string>()
//             };
//             failureResponse.Errors.Add(message);
//             return failureResponse;
//         }
//
//         ValidationResult validationResult = await createAlbumValidator.ValidateAsync(createAlbumRequest);
//
//         if (!validationResult.IsValid)
//         {
//             var failureResponse = new CreateAlbumResponse()
//             {
//                 StatusCode = false,
//                 Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList()
//             };
//             return failureResponse;
//         }
//         
//         var response = await request.GetResponse<CreateAlbumResponse>(createAlbumRequest);
//         return response.Message;
//     }
// }

