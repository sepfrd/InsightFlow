using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class Person : BaseEntityWithGuid
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public User? User { get; set; }
}