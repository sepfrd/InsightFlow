using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sieve.Attributes;

namespace InsightFlow.Model.BaseEntities;

public class BaseEntity
{
    protected BaseEntity() => CreationDate = LastUpdated = DateTime.Now;

    [Key]
    [Sieve(CanSort = true, CanFilter = true)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Sieve(CanSort = true, CanFilter = true)]
    public DateTime CreationDate { get; }

    [Sieve(CanSort = true, CanFilter = true)]
    public DateTime LastUpdated { get; set; }
}