using System.Net;
using AutoMapper;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.BaseEntities;
using Sieve.Models;

namespace InsightFlow.Business.Base;

public class BaseBusiness<TEntity, TDto> : IBaseBusiness<TDto>
    where TDto : BaseDto
    where TEntity : BaseEntityWithGuid
{
    private readonly IMapper _mapper;

    private readonly IAdminBaseBusiness<TEntity, TDto> _adminBaseBusiness;

    protected BaseBusiness(IAdminBaseBusiness<TEntity, TDto> adminBaseBusiness, IMapper mapper)
    {
        _adminBaseBusiness = adminBaseBusiness;

        _mapper = mapper;
    }

    public async Task<CustomResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _adminBaseBusiness.CreateAsync(dto, cancellationToken);

        if (!result.IsSuccess)
        {
            return CustomResponse<TDto>.CreateUnsuccessfulResponse(result.HttpStatusCode);
        }

        var entityDto = _mapper.Map<TDto>(result.Data);

        return CustomResponse<TDto>.CreateSuccessfulResponse(entityDto, httpStatusCode: HttpStatusCode.Created);
    }

    public async Task<CustomResponse<TDto>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var result = await _adminBaseBusiness.GetByGuidAsync(guid, cancellationToken);

        if (!result.IsSuccess)
        {
            return CustomResponse<TDto>.CreateUnsuccessfulResponse(result.HttpStatusCode);
        }

        var entityDto = _mapper.Map<TDto>(result.Data);

        return CustomResponse<TDto>.CreateSuccessfulResponse(entityDto);
    }

    public async Task<PagedCustomResponse<List<TDto>>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= 10;

        var result = await _adminBaseBusiness.GetAllAsync(sieveModel, cancellationToken);

        var dtos = _mapper.Map<List<TDto>>(result.Data);

        return PagedCustomResponse<List<TDto>>.CreateSuccessfulResponse(
            dtos,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            dtos.Count,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }

    public async Task<CustomResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var result = await _adminBaseBusiness.UpdateAsync(dto, cancellationToken);

        if (!result.IsSuccess)
        {
            return CustomResponse<TDto>.CreateUnsuccessfulResponse(result.HttpStatusCode);
        }

        var updatedDto = _mapper.Map<TDto>(result.Data);

        return CustomResponse<TDto>.CreateSuccessfulResponse(updatedDto);
    }

    public async Task<CustomResponse<TDto>> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        var result = await _adminBaseBusiness.DeleteByGuidAsync(guid, cancellationToken);

        if (!result.IsSuccess)
        {
            return CustomResponse<TDto>.CreateUnsuccessfulResponse(result.HttpStatusCode);
        }

        var deletedDto = _mapper.Map<TDto>(result.Data);

        return CustomResponse<TDto>.CreateSuccessfulResponse(deletedDto);
    }
}