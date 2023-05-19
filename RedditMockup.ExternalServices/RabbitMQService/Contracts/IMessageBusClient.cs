using RedditMockup.Common.Dtos;

namespace RedditMockup.ExternalService.RabbitMQService.Contracts;

public interface IMessageBusClient
{
    void PublishNewQuestion(QuestionPublishedDto questionPublishedDto);
}