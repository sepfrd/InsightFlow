using System.Net;
using AutoMapper;
using InsightFlow.Business.Base;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.Contracts;
using InsightFlow.DataAccess.Repositories;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace InsightFlow.Business.Businesses.AdminBusinesses;

public class AdminUserBusiness : AdminBaseBusiness<User, UserDto>
{
    private readonly UserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminUserBusiness(IUnitOfWork unitOfWork, IMapper mapper) :
        base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
        _userRepository = (UserRepository)unitOfWork.UserRepository!;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async override Task<CustomResponse<User?>> CreateAsync(UserDto answerDto, CancellationToken cancellationToken = default)
    {
        var person = new Person
        {
            FirstName = answerDto.FirstName,
            LastName = answerDto.LastName
        };

        var createdPerson = await _unitOfWork.PersonRepository!.CreateAsync(person, cancellationToken);

        answerDto.Password = await answerDto.Password!.GetHashStringAsync();

        var user = _mapper.Map<User>(answerDto);

        user.PersonId = createdPerson.Id;

        return await CreateAsync(user, cancellationToken);
    }

    public async override Task<CustomResponse<User?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, users =>
                users
                    .Include(user => user.Person)
                    .Include(userEntity => userEntity.Profile),
            cancellationToken);

        if (user is null)
        {
            return CustomResponse<User?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<User?>.CreateSuccessfulResponse(user);
    }

    public async override Task<CustomResponse<User?>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByGuidAsync(guid, users =>
                users
                    .Include(userEntity => userEntity.Person)
                    .Include(user => user.Profile),
            cancellationToken);

        if (user is null)
        {
            return CustomResponse<User?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<User?>.CreateSuccessfulResponse(user);
    }

    public async override Task<PagedCustomResponse<List<User>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= 10;

        var result = await _userRepository.GetAllAsync(
            sieveModel, users =>
                users
                    .Include(user => user.Person)
                    .Include(user => user.Profile),
            cancellationToken);

        var currentCount = result.Entities?.Count ?? 0;

        return PagedCustomResponse<List<User>?>.CreateSuccessfulResponse(
            result.Entities,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            currentCount,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }
}