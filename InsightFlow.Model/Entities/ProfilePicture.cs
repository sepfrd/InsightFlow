namespace InsightFlow.Model.Entities;

public class ProfilePicture : BaseEntity
{
    public byte[]? PictureBytes { get; set; }

    public int ProfileId { get; set; }

    public Profile? Profile { get; set; }
}