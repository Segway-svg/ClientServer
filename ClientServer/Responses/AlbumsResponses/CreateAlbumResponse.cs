namespace ClientServer.Responses.AlbumsResponses;

public class CreateAlbumResponse
{
    public Guid Id { get; set; }
    public bool StatusCode { get; set; }
    public List<string> Errors { get; set; }
}