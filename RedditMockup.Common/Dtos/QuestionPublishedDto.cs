namespace RedditMockup.Common.Dtos;

public class QuestionPublishedDto
{
    public QuestionPublishedDto(QuestionDto questionDto, string publishEvent)
    {
        QuestionDto = questionDto;

        Event = publishEvent;
    }

    public QuestionPublishedDto()
    {
    }
    
    public QuestionDto? QuestionDto { get; set; }

    public string? Event { get; set; }
}

