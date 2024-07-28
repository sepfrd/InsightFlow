using RedditMockup.Model.BaseEntities;

namespace RedditMockup.Model.Entities;

public class Role : BaseEntityWithGuid
{
    public string? Title { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}