//produser
using MassTransistRabbitMqMy;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                        x.AddConsumer<PingConsumer>();
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
