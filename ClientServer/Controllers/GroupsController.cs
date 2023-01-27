using ClientServer.Requests;
using ClientServer.Responses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ClientServer.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    [HttpPost("post")]
    public async Task<bool> PostGroup(
        [FromServices] IRequestClient<PostUserRequest> requestClient,
        [FromBody] PostUserRequest request)
    {
        var response = await requestClient.GetResponse<PostUserResponse>(request);
        return response.Message.IsSuccess;
    }
}