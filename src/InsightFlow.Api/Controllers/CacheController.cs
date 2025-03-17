using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/cache-management")]
public class CacheController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<CacheController> _logger;

    public CacheController(IRoleService roleService, ILogger<CacheController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    [HttpPost]
    [Route("roles/refresh")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<IActionResult> RefreshRolesCacheAsync(CancellationToken cancellationToken)
    {
        var result = await _roleService.InitializeAsync(cancellationToken);

        if (result)
        {
            return Ok(StringConstants.RoleServiceInitializationSuccessLog);
        }

        var message = string.Format(StringConstants.RoleServiceInitializationFailureLog, 1);

        _logger.LogCritical(StringConstants.ApplicationInternalServerErrorTemplate, message);

        return StatusCode(StatusCodes.Status500InternalServerError, message);
    }
}