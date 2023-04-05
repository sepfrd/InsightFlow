using RedditMockup.Business.Contracts;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Base;

public abstract class BaseBusiness<T> : IBaseBusiness<T>
    where T : BaseEntity
{
    #region [Fields]

    private readonly IUnitOfWork _unitOfWork;

    private readonly IBaseRepository<T> _repository;

    #endregion

    #region [Constructor]

    protected BaseBusiness(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    #endregion

    #region [Methods]

    public async Task<T?> CreateAsync(T t, CancellationToken cancellationToken = new())
    {
        var entity = await _repository.CreateAsync(t, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<T>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = new()) =>
        await _repository.LoadAllAsync(sieveModel, null, cancellationToken);

    public async Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken = new()) =>
        await _repository.LoadByIdAsync(id, cancellationToken);

    public async Task<T?> UpdateAsync(T t, CancellationToken cancellationToken = new())
    {
        T entity = _repository.Update(t);

        await _unitOfWork.CommitAsync(cancellationToken);

        return entity;
    }

    public async Task<T?> DeleteAsync(int id, CancellationToken cancellationToken = new())
    {
        T? entity = await _repository.LoadByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        T deletedEntity = _repository.Delete(entity);

        await _unitOfWork.CommitAsync(cancellationToken);

        return deletedEntity;
    }

    #endregion
}