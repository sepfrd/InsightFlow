namespace InsightFlow.Model.Entities;

public class Person : BaseEntity
{
    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    public User? User { get; set; }
}