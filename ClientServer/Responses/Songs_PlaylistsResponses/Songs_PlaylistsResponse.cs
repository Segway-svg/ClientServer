namespace ClientServer.Responses.Songs_PlaylistsResponses;

public class Songs_PlaylistsResponse
{
    public Guid Id { get; set; }
    public Guid PlaylistId { get; set; }
    public Guid SongId { get; set; }
    public bool IsSuccess { get; set; }
}