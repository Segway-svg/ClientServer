using ClientServer.Requests.AlbumsRequests;
using ClientServer.Responses.AlbumsResponses;

namespace ClientServer.Commands.AlbumsCommands.Create;

public interface ICreateAlbumCommand
{
    public Task<CreateAlbumResponse> Execute(CreateAlbumRequest createAlbumRequest);
}