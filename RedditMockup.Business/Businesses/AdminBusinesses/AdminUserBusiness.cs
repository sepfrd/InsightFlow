using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Businesses.AdminBusinesses;

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

    public async override Task<CustomResponse<List<User>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(sieveModel, users =>
                users
                    .Include(user => user.Person)
                    .Include(user => user.Profile),
            cancellationToken);

        return CustomResponse<List<User>?>.CreateSuccessfulResponse(users);
    }
}