namespace RedditMockup.Common.Dtos;

public class QuestionPublishedDto
{
    public QuestionPublishedDto(QuestionDto questionDto, string eventType)
    {
        QuestionDto = questionDto;

        Event = eventType;
    }

    public QuestionPublishedDto()
    {
    }
    
    public QuestionDto? QuestionDto { get; set; }

    public string? Event { get; set; }
}

