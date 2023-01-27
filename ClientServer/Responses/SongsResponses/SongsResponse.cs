namespace ClientServer.Responses.SongsResponses;

public class SongsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsFamous { get; set; }
    public Guid AlbumId { get; set; }
    public bool IsSuccess { get; set; }
}