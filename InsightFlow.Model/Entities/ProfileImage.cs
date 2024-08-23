namespace InsightFlow.Model.Entities;

public class ProfileImage : BaseEntity
{
    public byte[]? ImageBytes { get; set; }

    public string? ImageFormat { get; set; }

    public int ProfileId { get; set; }

    public Profile? Profile { get; set; }
}