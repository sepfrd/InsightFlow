using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.DomainEntityBusinesses;

public class UserBusiness : BaseBusiness<User, UserDto>
{
    // [Fields]

    private readonly UserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // --------------------------------------

    // [Constructor]

    public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper) :
        base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
        _userRepository = unitOfWork.UserRepository!;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // --------------------------------------

    // [Methods]

    public async override Task<User?> CreateAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var person = new Person
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };

        var createdPerson = await _unitOfWork.PersonRepository!.CreateAsync(person, cancellationToken);

        userDto.Password = await userDto.Password!.GetHashStringAsync();

        var user = _mapper.Map<User>(userDto);

        user.PersonId = createdPerson.Id;

        return await CreateAsync(user, cancellationToken);
    }

    public async override Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _userRepository.GetByIdAsync(id,
            users => users.Include(user => user.Person)
                .Include(user => user.Profile),
            cancellationToken);

    public async override Task<User?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default) =>
        await _userRepository.GetByGuidAsync(guid,
            users => users.Include(user => user.Person)
                .Include(user => user.Profile),
            cancellationToken);

    public async override Task<List<User>?> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default) =>
        await _userRepository.GetAllAsync(sieveModel,
            users => users.Include(user => user.Person)
                .Include(user => user.Profile),
            cancellationToken);

    // --------------------------------------
}