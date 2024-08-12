using System.Net;
using AutoMapper;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;

namespace RedditMockup.Business.Base;

public abstract class AdminBaseBusiness<TEntity, TDto> : IAdminBaseBusiness<TEntity, TDto>
    where TEntity : BaseEntityWithGuid
    where TDto : BaseDto
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IBaseRepository<TEntity> _repository;

    private readonly IMapper _mapper;

    protected AdminBaseBusiness(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;

        _repository = repository;

        _mapper = mapper;
    }

    public abstract Task<CustomResponse<TEntity?>> CreateAsync(TDto answerDto, CancellationToken cancellationToken = default);

    protected async Task<CustomResponse<TEntity?>> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse<TEntity?>.CreateSuccessfulResponse(createdEntity, httpStatusCode: HttpStatusCode.Created);
    }

    public abstract Task<CustomResponse<TEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    public abstract Task<CustomResponse<TEntity?>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    public abstract Task<CustomResponse<List<TEntity>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    public async Task<CustomResponse<TEntity?>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var result = await GetByGuidAsync(dto.Guid, cancellationToken);

        if (result.Data is null)
        {
            return result;
        }

        _mapper.Map(dto, result.Data);

        var updatedEntity = _repository.Update(result.Data);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse<TEntity?>.CreateSuccessfulResponse(updatedEntity);
    }

    public async Task<CustomResponse<TEntity?>> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, null, cancellationToken);

        if (entity is null)
        {
            return CustomResponse<TEntity?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        var deletedEntity = _repository.Delete(entity);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse<TEntity?>.CreateSuccessfulResponse(deletedEntity);
    }

    public async Task<CustomResponse<TEntity?>> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var result = await GetByGuidAsync(guid, cancellationToken);

        if (result.Data is null)
        {
            return result;
        }

        var deletedEntity = _repository.Delete(result.Data);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse<TEntity?>.CreateSuccessfulResponse(deletedEntity);
    }
}