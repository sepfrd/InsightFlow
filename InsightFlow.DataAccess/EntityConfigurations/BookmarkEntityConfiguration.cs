using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class BookmarkEntityConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder
            .HasOne<User>(bookmark => bookmark.User)
            .WithMany(user => user.Bookmarks)
            .HasForeignKey(bookmark => bookmark.UserId);

        builder
            .HasOne<Question>(bookmark => bookmark.Question)
            .WithMany(question => question.Bookmarks)
            .HasForeignKey(bookmark => bookmark.QuestionId);
    }
}