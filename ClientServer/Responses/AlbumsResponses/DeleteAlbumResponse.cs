namespace ClientServer.Responses.AlbumsResponses;

public class DeleteAlbumResponse
{
    public bool StatusCode { get; set; }
    public List<string> Errors { get; set; }
}