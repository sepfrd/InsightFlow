using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedditMockup.Common.Constants;
using RedditMockup.DataAccess.Context;

namespace RedditMockup.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(serviceDescriptor =>
                serviceDescriptor.ServiceType == typeof(RedditMockupDbContext));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<RedditMockupDbContext>(options =>
                options.UseInMemoryDatabase(ApplicationConstants.ApplicationName));
        });

        builder.UseEnvironment(ApplicationConstants.TestingEnvironmentName);
    }
}