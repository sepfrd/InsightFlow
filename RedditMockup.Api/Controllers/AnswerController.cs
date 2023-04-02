using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class AnswerController : BaseController<Answer>
{
    private readonly AnswerBusiness _answerBusiness;

    public AnswerController(IBaseBusiness<Answer> business) : base(business) =>
        _answerBusiness = (AnswerBusiness)business;

}