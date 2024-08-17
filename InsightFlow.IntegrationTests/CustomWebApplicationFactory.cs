using System.Linq;
using InsightFlow.Common.Constants;
using InsightFlow.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsightFlow.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(serviceDescriptor =>
                serviceDescriptor.ServiceType == typeof(InsightFlowDbContext));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<InsightFlowDbContext>(options =>
                options.UseInMemoryDatabase(ApplicationConstants.ApplicationName));
        });

        builder.UseEnvironment(ApplicationConstants.TestingEnvironmentName);
    }
}