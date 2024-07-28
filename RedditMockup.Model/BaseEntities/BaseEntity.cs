using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sieve.Attributes;

namespace RedditMockup.Model.BaseEntities;

public class BaseEntity
{
    protected BaseEntity() => CreationDate = LastUpdated = DateTime.Now;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Sieve(CanSort = true)]
    public int Id { get; init; }

    [Sieve(CanSort = true)]
    public DateTime CreationDate { get; }

    [Sieve(CanSort = true)]
    public DateTime LastUpdated { get; set; }
}