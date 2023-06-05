using AutoMapper;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.Base;

public class DtoBaseBusiness<TEntity, TDto> : IDtoBaseBusiness<TDto>
    where TDto : BaseDto
    where TEntity : BaseEntityWithGuid
{
    #region [Fields]

    private readonly IMapper _mapper;

    private readonly IBaseBusiness<TEntity, TDto> _baseBusiness;

    #endregion

    protected DtoBaseBusiness(IBaseBusiness<TEntity, TDto> baseBusiness, IMapper mapper)
    {
        _baseBusiness = baseBusiness;

        _mapper = mapper;
    }

    public async Task<CustomResponse<TDto>> PublicCreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        TEntity? entity = await _baseBusiness.CreateAsync(dto, cancellationToken);

        if (entity is null)
        {
            return new CustomResponse<TDto>
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        var entityDto = _mapper.Map<TDto>(entity);

        return new CustomResponse<TDto>
        {
            Data = entityDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.Created
        };

    }

    public async Task<CustomResponse<TDto>> PublicGetByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        TEntity? entity = await _baseBusiness.GetByGuidAsync(guid, null, cancellationToken);

        if (entity is null)
        {
            return CustomResponse<TDto>.CustomNotFoundResponse;
        }

        var entityDto = _mapper.Map<TDto>(entity);

        return new CustomResponse<TDto>
        {
            Data = entityDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<List<TDto>>> PublicGetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var entities = await _baseBusiness.GetAllAsync(sieveModel, null, cancellationToken);

        var dtos = _mapper.Map<List<TDto>>(entities);

        return new CustomResponse<List<TDto>>
        {
            Data = dtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };

    }

    public async Task<CustomResponse<TDto>> PublicUpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _baseBusiness.UpdateAsync(dto, cancellationToken);

        if (entity is null)
        {
            return CustomResponse<TDto>.CustomNotFoundResponse;
        }

        var updatedDto = _mapper.Map<TDto>(entity);

        return new CustomResponse<TDto>
        {
            Data = updatedDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<TDto>> PublicDeleteByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        TEntity? deletedEntity = await _baseBusiness.DeleteByGuidAsync(guid, cancellationToken);

        if (deletedEntity is null)
        {
            return CustomResponse<TDto>.CustomNotFoundResponse;
        }

        var deletedDto = _mapper.Map<TDto>(deletedEntity);

        return new CustomResponse<TDto>
        {
            Data = deletedDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}


/*
    #region [Methods]

    public virtual async Task<CustomResponse<TDto>> PublicCreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var baseEntity = _mapper.Map<TBase>(dto);

        baseEntity = await CreateAsync(baseEntity, cancellationToken);

        if (baseEntity is null)
        {
            return new CustomResponse<TDto>
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError,
            };
        }

        var tDto = _mapper.Map<TDto>(baseEntity);

        return new CustomResponse<TDto>
        {
            Data = tDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.Created
        };
    }

    public new async Task<CustomResponse<TDto>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var baseEntity = await base.DeleteByIdAsync(id, cancellationToken);

        if (baseEntity is null)
        {
            return new CustomResponse<TDto>
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var dto = _mapper.Map<TDto>(baseEntity);

        return new CustomResponse<TDto>
        {
            Data = dto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<IEnumerable<TDto>>> PublicGetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var baseEntities = await base.GetAllAsync(sieveModel, cancellationToken);

        var dtos = _mapper.Map<IEnumerable<TDto>>(baseEntities);

        return new CustomResponse<IEnumerable<TDto>>
        {
            Data = dtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<TDto>> PublicGetByGuidAsync(int id, CancellationToken cancellationToken)
    {
        var baseEntity = await base.GetByIdAsync(id, cancellationToken);

        if (baseEntity is null)
        {
            return new CustomResponse<TDto>
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var dto = _mapper.Map<TDto>(baseEntity);

        return new CustomResponse<TDto>
        {
            Data = dto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var baseEntity = _mapper.Map<TBase>(dto);

        baseEntity = await UpdateAsync(baseEntity, cancellationToken);

        if (baseEntity is null)
        {
            return new CustomResponse<TDto>
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError,
            };
        }

        var tDto = _mapper.Map<TDto>(baseEntity);

        return new CustomResponse<TDto>
        {
            Data = tDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion*/