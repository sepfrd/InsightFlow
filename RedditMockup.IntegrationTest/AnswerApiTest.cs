using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RedditMockup.Common.Dtos;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RedditMockup.IntegrationTest;

public class AnswerApiTest : IClassFixture<WebApplicationFactory<Program>>
{

    #region [Field(s)]

    private const string BaseAddress = "/api/Answer";

    private readonly WebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;




    #endregion

    #region [Constructor]

    public AnswerApiTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));

        _client = _factory.CreateClient();
    }

    #endregion

    #region [Method(s)]

    private async Task AuthenticateAsync()
    {


        var _loginDto = new LoginDto()
        {
            Username = "sepehr_frd",
            Password = "sfr1376",
            RememberMe = true
        };

        var serializedLoginDto = JsonSerializer.Serialize(_loginDto);

        var stringContent = new StringContent(serializedLoginDto, Encoding.UTF8, "application/json");

        await _client.PostAsync($"{BaseAddress}/Login", stringContent);
    }

    #endregion

    #region [Theory Method(s)]

    [Fact]
    public async Task GetAll_ReturnExpectedResult()
    {
        #region [Act]

        var response = await _client.GetAsync(BaseAddress);

        var streamResponse = await response.Content.ReadAsStringAsync();

        var apiResponse = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<SamanSalamatResponse<List<AnswerDto>>>(streamResponse));

        #endregion

        #region [Assert]
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        apiResponse?.Data?.Should().BeOfType<List<AnswerDto>>();
        #endregion
    }

    #endregion

    #region [Data Method(s)]

    #endregion
}
