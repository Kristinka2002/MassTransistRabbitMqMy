using MassTransit;
using Microsoft.Extensions.Logging;

namespace PingConsumer
{
    public class PingConsumer : IConsumer<Ping>
    {
        readonly ILogger<PingConsumer> _logger;

        public PingConsumer(ILogger<PingConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<Ping> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Buttom);

            return Task.CompletedTask;
        }
    }
    public record Ping(string Buttom);
}
