namespace ClientServer.Responses.AlbumsResponses;

public class GetAlbumResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool StatusCode { get; set; }
    public List<string> Errors { get; set; }
}