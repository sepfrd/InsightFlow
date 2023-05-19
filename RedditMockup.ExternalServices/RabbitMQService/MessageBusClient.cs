using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RedditMockup.Common.Dtos;
using RedditMockup.ExternalService.RabbitMQService.Contracts;

namespace RedditMockup.ExternalService.RabbitMQService;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly IConfiguration _configuration;

    private readonly ILogger _logger;

    private readonly IConnection _connection;

    private readonly IModel _channel;

    private const string _triggerExchange = "trigger";

    public MessageBusClient(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;

        _logger = logger;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = _configuration.GetSection("RabbitMQ").GetValue<string>("RabbitMQHost"),
            Port = _configuration.GetSection("RabbitMQ").GetValue<int>("RabbitMQPort")
        };

        try
        {
            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _triggerExchange, type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQConnectionShutdownEventHandler;

            _logger.LogInformation("Connected to the Message Bus");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception thrown while trying to connect to the Message Bus.");
            throw;
        }
    }

    public void PublishNewQuestion(QuestionPublishedDto questionPublishedDto)
    {
        var message = JsonSerializer.Serialize(questionPublishedDto);

        if (_connection.IsOpen)
        {
            SendMessage(message);
        }
        else
        {
            _logger.LogError("Message not sent due to message bus connection being closed.");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: _triggerExchange,
            routingKey: "",
            basicProperties: null,
            body: body);

        _logger.LogInformation($"{message} was sent over {_triggerExchange} exchange");
    }

    private void RabbitMQConnectionShutdownEventHandler(object? sender, ShutdownEventArgs shutdownEventArgs)
    {
        _logger.LogInformation($"RabbitMQ connection was shutdown by {sender}", shutdownEventArgs);
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }

        _logger.LogInformation("Message Bus Disposed");
    }
}

