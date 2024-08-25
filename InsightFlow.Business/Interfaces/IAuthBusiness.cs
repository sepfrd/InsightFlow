using DNTCaptcha.Core;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;

namespace InsightFlow.Business.Interfaces;

public interface IAuthBusiness
{
    Task<CustomResponse<string>> LoginAsync(CaptchaDto captchaDto, LoginDto loginDto, CancellationToken cancellationToken = default);

    DNTCaptchaApiResponse CreateCaptcha();
}