using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
//using StackExchange.Redis;

namespace RedditMockup.Business.EntityBusinesses;

public class UserBusiness : BaseBusiness<User, UserDto>
{
    #region [Redis Section]

    //private readonly IConnectionMultiplexer _connectionMultiplexer;

    //private readonly IDatabase _redisDb;


    /*
       public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMultiplexer connectionMultiplexer) :
            base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
        _userRepository = unitOfWork.UserRepository!;
        Mapper = mapper;
        _connectionMultiplexer = connectionMultiplexer;
        _redisDb = _connectionMultiplexer.GetDatabase();
    }

    */

    //var key = $"User {user.Id}";

    //var value = user.Username;

    //await _redisDb.StringSetAsync(key, value);

    #endregion

    #region [Constructor]

    public UserBusiness(IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, unitOfWork.UserRepository!, mapper)
    {
    }

    #endregion
}