namespace ClientServer.Requests.AlbumsRequests;

public class UpdateAlbumRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}