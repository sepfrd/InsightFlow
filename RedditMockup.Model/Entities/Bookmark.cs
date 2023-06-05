using RedditMockup.Model.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class Bookmark : BaseEntity
{
    #region [Properties]
    public bool IsBookmarked { get; set; }

    #endregion

    #region [Navigation Properties]

    public int UserId { get; set; }

    public User? User { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }

    #endregion
}