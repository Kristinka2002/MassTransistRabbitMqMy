//consumer
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PingConsumer;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {


                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("rabbitmq://localhost", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                        x.AddConsumer<PingConsumer.PingConsumer>();
                        cfg.ConfigureEndpoints(context);


                        // cfg.ReceiveEndpoint("ping-queue", e =>
                        // {
                        ////    e.Bind("ping-exchange");
                        ////    e.Bind<Ping>();
                        // });
                    });
                });
                services.AddHostedService<PingPublisher>();

            });
}

//**** if it will be web

//var builder = WebApplication.CreateBuilder();
//builder.Services.AddMassTransit(x =>
//{
//    x.SetKebabCaseEndpointNameFormatter();
//    x.AddConsumer<PingConsumer.PingConsumer>();
//    x.UsingRabbitMq((context, cfg) =>
//    {


//        cfg.Host("localhost", "/", h =>
//        {
//            h.Username("guest");
//            h.Password("guest");

//        });
//        cfg.ConfigureEndpoints(context);
//    });
//});
////builder.Services.AddHostedService<PingPublisher>();

//var app = builder.Build();
//app.Run();
