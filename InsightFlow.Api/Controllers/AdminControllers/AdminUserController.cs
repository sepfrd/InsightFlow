using InsightFlow.Api.Base;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers.AdminControllers;

[ApiController]
[Route("api/admin/users", Name = "Admin - Users")]
public class AdminUserController : AdminBaseController<User, UserDto>
{
    public AdminUserController(IAdminBaseBusiness<User, UserDto> business) : base(business)
    {
    }
}