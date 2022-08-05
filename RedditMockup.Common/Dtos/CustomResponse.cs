namespace RedditMockup.Common.Dtos;

public class CustomResponse<T>
{

    public T? Data { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

}

public class CustomResponse
{

    public dynamic? Data { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

}