using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
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

    private readonly IMapper _mapper;

    #endregion

    #region [Constructor]

    protected BaseBusiness(IUnitOfWork unitOfWork, IBaseRepository<T> repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region [Methods]

    public async Task<T?> CreateAsync(T t, CancellationToken cancellationToken = new())
    {
        var entity = await _repository.CreateAsync(t, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var response = _mapper.Map<DTO>(entity);

        return new CustomResponse
        {
            Data = response,
            IsSuccess = true,
            Message = "Entity Saved"
        };
    }

    public virtual async Task<IEnumerable<T>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = new())
    {
        var data = await _repository.LoadAllAsync(sieveModel, null, cancellationToken);

        var result = _mapper.Map<IEnumerable<DTO>>(data);

        return new CustomResponse<IEnumerable<DTO>>
        {
            Data = result,
            Message = "Data Loaded",
            IsSuccess = true
        };
    }

    public async Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken = new()) =>
        await _repository.LoadByIdAsync(id, cancellationToken);


    public async Task<T?> UpdateAsync(T t, CancellationToken cancellationToken = new())
    {
        _repository.Update(t);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Entity Updated"
        };
    }

    public async Task<T?> DeleteAsync(T t, CancellationToken cancellationToken = new())
    {
        _repository.Delete(t);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Entity Deleted"
        };
    }


    #endregion

    #region [Abstract Methods]

    //public abstract Task<CustomResponse?> CreateAsync(DTO dto, HttpContext httpContext, CancellationToken
    //cancellationToken = new());


    //public abstract Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken cancellationToken = new());

    //public abstract Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken = new());

    #endregion
}