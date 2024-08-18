using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightFlow.Model.BaseEntities;

public class BaseEntity
{
    protected BaseEntity() => CreationDate = LastUpdated = DateTime.Now;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public DateTime CreationDate { get; }

    public DateTime LastUpdated { get; set; }
}