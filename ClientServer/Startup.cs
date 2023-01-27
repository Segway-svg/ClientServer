using ClientServer.Commands.AlbumsCommands.Create;
using ClientServer.Requests;
using ClientServer.Requests.AlbumsRequests;
using ClientServer.Validators.AlbumsRequestsValidators.Create;
using MassTransit;

namespace ClientServer;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Формируем список используемых служб/сервисов
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ICreateAlbumValidator, CreateAlbumValidator>();
        services.AddTransient<ICreateAlbumCommand, CreateAlbumCommand>();
        
        services.AddControllers();
        
        services.AddMassTransit(mt =>
        { 
            mt.UsingRabbitMq((context, config) =>
            {
                config.Host("localhost", "/", host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
            });
            mt.AddRequestClient<PostUserRequest>(new Uri("rabbitmq://localhost/postUserKey"));
            mt.AddRequestClient<CreateAlbumRequest>(new Uri("rabbitmq://localhost/create"));
            mt.AddRequestClient<GetAlbumRequest>(new Uri("rabbitmq://localhost/get"));
            mt.AddRequestClient<UpdateAlbumRequest>(new Uri("rabbitmq://localhost/update"));
            mt.AddRequestClient<DeleteAlbumRequest>(new Uri("rabbitmq://localhost/delete"));
        });
        services.AddMassTransitHostedService();
    }

    public void Configure(IApplicationBuilder app)
    {
        // Чтобы запрос пришёл куда надо
        app.UseRouting();

        // Настройка HTTP запроса 
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}