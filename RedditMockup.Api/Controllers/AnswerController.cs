using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class AnswerController : BaseController<Answer>
{
    public AnswerController(IBaseBusiness<Answer> business) : base(business)
    {
    }
}