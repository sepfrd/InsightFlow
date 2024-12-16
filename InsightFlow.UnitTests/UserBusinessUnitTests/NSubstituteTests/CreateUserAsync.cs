using System.Net;
using AutoMapper;
using InsightFlow.Business.Businesses;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Common.Profiles;
using InsightFlow.DataAccess.Dtos;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using InsightFlow.UnitTests.Common.FakeDataGenerators;
using InsightFlow.UnitTests.Common.NSubstituteHelpers;
using NSubstitute;

namespace InsightFlow.UnitTests.UserBusinessUnitTests.NSubstituteTests;

public class CreateUserAsync
{
    [Theory]
    [MemberData(nameof(CreateUserAsyncTheoryData))]
    public async Task CreateUserAsync_ShouldSuccess_WhenUserIsValid(User user)
    {
        // Arrange

        var createUserRequestDto = new CreateUserRequestDto(
            user.Username,
            user.Email,
            user.Password,
            user.FirstName,
            user.LastName,
            user.Email);

        var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();

        unitOfWorkSubstitute.SetupCommitAsyncToReturn(1);

        var userRepositorySubstitute = Substitute.For<IBaseRepository<User>>();

        userRepositorySubstitute
            .SetupCreateAsyncToReturn(user)
            .SetupGetAllAsyncToReturn(new PagedEntitiesResponseDto<User>([]));

        unitOfWorkSubstitute.UserRepository.Returns(userRepositorySubstitute);

        var authBusinessSubstitute = Substitute.For<IAuthBusiness>();

        var mapperConfig = new MapperConfiguration(configExpression => configExpression.AddProfile<UserProfile>());
        var mapper = mapperConfig.CreateMapper();

        var userBusiness = new UserBusiness(unitOfWorkSubstitute, mapper, authBusinessSubstitute);

        // Act

        var customResponse = await userBusiness.CreateUserAsync(createUserRequestDto);

        var userDto = mapper.Map<UserDto>(user);

        //Assert

        Assert.NotNull(customResponse.Data);
        Assert.Null(customResponse.Message);
        Assert.True(customResponse.IsSuccess);
        Assert.Equal(HttpStatusCode.Created, customResponse.HttpStatusCode);
        Assert.Equivalent(userDto, customResponse.Data);
    }

    public static TheoryData<User> CreateUserAsyncTheoryData()
    {
        var theoryData = new TheoryData<User>();

        FakeUserGenerator.GenerateFakeUsers(10).ForEach(theoryData.Add);

        return theoryData;
    }
}