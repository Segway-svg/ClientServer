namespace ClientServer.Requests;

public class Songs_PlaylistsRequest
{
    public Guid Id { get; set; }
    public Guid PlaylistId { get; set; }
    public Guid SongId { get; set; }
}