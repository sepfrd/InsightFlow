using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;

namespace RedditMockup.Business.Base;

public class BaseBusiness<TEntity, TDto> : IBaseBusiness<TEntity, TDto>
    where TEntity : BaseEntityWithGuid
    where TDto : BaseDto
{
    #region [Fields]

    private readonly IUnitOfWork _unitOfWork;

    private readonly IBaseRepository<TEntity> _repository;

    private readonly IMapper _mapper;

    #endregion

    #region [Constructor]

    protected BaseBusiness(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;

        _repository = repository;

        _mapper = mapper;
    }

    #endregion

    #region [Methods]

    public async Task<TEntity?> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var t = _mapper.Map<TEntity>(dto);

        TEntity createdEntity = await _repository.CreateAsync(t, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return createdEntity;
    }

    public async Task<TEntity?> GetByIdAsync(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken cancellationToken = default) =>
        await _repository.GetByIdAsync(id, include, cancellationToken);

    public async Task<TEntity?> GetByGuidAsync(Guid guid, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken cancellationToken =
        default) =>
        await _repository.GetByGuidAsync(guid, include, cancellationToken);

    public async Task<List<TEntity>?> GetAllAsync(SieveModel sieveModel, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, CancellationToken
        cancellationToken = default) =>
        await _repository.GetAllAsync(sieveModel, include, cancellationToken);

    public async Task<TEntity?> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        TEntity? t = await GetByGuidAsync(dto.Guid, null, cancellationToken);

        if (t is null)
        {
            return null;
        }

        _mapper.Map(dto, t);

        TEntity updatedEntity = _repository.Update(t);

        await _unitOfWork.CommitAsync(cancellationToken);

        return updatedEntity;
    }

    public async Task<TEntity?> DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, null, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        TEntity deletedEntity = _repository.Delete(entity);

        await _unitOfWork.CommitAsync(cancellationToken);

        return deletedEntity;
    }

    public async Task<TEntity?> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await GetByGuidAsync(guid, null, cancellationToken);

        return entity is null ? null : _repository.Delete(entity);
    }

    #endregion
}