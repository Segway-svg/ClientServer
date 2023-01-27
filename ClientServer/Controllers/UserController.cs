// // using ClientServer.Requests;
// // using ClientServer.Responses;
// // using MassTransit;
// // using Microsoft.AspNetCore.Mvc;
// //
// // namespace ClientServer.Controllers;
// //
// // [ApiController]
// // [Route("[controller]")]
// // public class UserController : ControllerBase
// // {
// //     [HttpPost("post")]
// //     public async Task<bool> PostUserController(
// //         [FromServices] IRequestClient<PostUserRequest> requestClient,
// //         [FromBody] PostUserRequest request)
// //     {
// //         var response = await requestClient.GetResponse<PostUserResponse>(request);
// //         return response.Message.IsSuccess;
// //     }
// // }
// using ClientServer.Requests;
// using ClientServer.Responses;
// using MassTransit;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ClientServer.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class UserController : ControllerBase
// {
//     [HttpPost("post")]
//     public async Task<PostUserResponse> PostUserController(
//         [FromServices] IRequestClient<PostUserRequest> requestClient,
//         [FromBody] PostUserRequest request)
//     {
//         var response = await requestClient.GetResponse<PostUserResponse>(request);
//         return response.Message;
//     }
// }

// using ClientServer.Requests;
// using ClientServer.Responses;
// using MassTransit;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ClientServer.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class UserController : ControllerBase
// {
//     [HttpPost("post")]
//     public async Task<bool> PostUserController(
//         [FromServices] IRequestClient<PostUserRequest> requestClient,
//         [FromBody] PostUserRequest request)
//     {
//         var response = await requestClient.GetResponse<PostUserResponse>(request);
//         return response.Message.IsSuccess;
//     }
// }
using ClientServer.Requests;
using ClientServer.Responses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ClientServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("post")]
    public async Task<PostUserResponse> PostUserController(
        [FromServices] IRequestClient<PostUserRequest> requestClient,
        [FromBody] PostUserRequest request)
    {
        var response = await requestClient.GetResponse<PostUserResponse>(request);
        return response.Message;
    }
}