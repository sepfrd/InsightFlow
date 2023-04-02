using RedditMockup.Business.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Base;

public class PublicBaseBusiness<T, TBase> : IBaseBusiness<T> 
    where TBase : BaseEntity
{
    private readonly IBaseBusiness<TBase> _baseBusiness;

    public PublicBaseBusiness(IBaseBusiness<TBase> baseBusiness) => 
        _baseBusiness = baseBusiness;
    

    public Task<T?> CreateAsync(T t, CancellationToken cancellationToken)
    {
            
    }

    public Task<T?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<T?> UpdateAsync(T t, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
