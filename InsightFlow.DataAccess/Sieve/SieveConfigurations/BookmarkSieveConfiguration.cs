using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class BookmarkSieveConfiguration : BaseSieveConfiguration<Bookmark>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Bookmark>(entity => entity.IsBookmarked)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Bookmark>(entity => entity.UserId)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Bookmark>(entity => entity.QuestionId)
            .CanSort()
            .CanFilter();
    }
}