using System.Net;
using ClientServer.Commands.AlbumsCommands;
using ClientServer.Commands.AlbumsCommands.Create;
using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ClientServer.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    [HttpPost("create")]
    public async Task<CreateAlbumResponse> CreateAlbumController(
        [FromServices] ICreateAlbumCommand createAlbumCommand,
        [FromBody] CreateAlbumRequest request)
    {
        var response = await createAlbumCommand.Execute(request);

        if (response.StatusCode)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        
        return response;
    }
    
    [HttpPost("get")]
    public async Task<GetAlbumResponse> GetAlbumController(
        [FromServices] IRequestClient<GetAlbumRequest> requestClient,
        [FromBody] GetAlbumRequest request)
    {
        GetAlbumCommand getAlbumCommand = new GetAlbumCommand();
        var response = await getAlbumCommand.Execute(requestClient, request);
    
        if (response.StatusCode)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        
        return response;
    }
    
    [HttpPost("update")]
    public async Task<UpdateAlbumResponse> UpdateAlbumController(
        [FromServices] IRequestClient<UpdateAlbumRequest> requestClient,
        [FromBody] UpdateAlbumRequest request)
    {
        UpdateAlbumCommand updateAlbumCommand = new UpdateAlbumCommand();
        var response = await updateAlbumCommand.Execute(requestClient, request);
    
        if (response.StatusCode)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        
        return response;
    }
    
    [HttpPost("delete")]
    public async Task<DeleteAlbumResponse> DeleteAlbumController(
        [FromServices] IRequestClient<DeleteAlbumRequest> requestClient,
        [FromBody] DeleteAlbumRequest request)
    {
        DeleteAlbumCommand deleteAlbumCommand = new DeleteAlbumCommand();
        var response = await deleteAlbumCommand.Execute(requestClient, request);
    
        if (response.StatusCode)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        
        return response;
    }
}