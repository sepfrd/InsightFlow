using RedditMockup.Business.Base;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Business.Businesses;

public class BookmarkBusiness : BaseBusiness<Bookmark>
{
    public BookmarkBusiness(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.BookmarkRepository!)
    {
    }
}
