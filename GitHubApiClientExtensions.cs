using System;
using GitHubApiClient.Abstractions;
using GitHubApiClient.Options;
using GitHubApiClient.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GitHubApiClient
{
    public static class GitHubApiClientExtensions
    {
        public static IServiceCollection AddGitHubApiClient(
            this IServiceCollection services, 
            Action<AddGitHubApiClientOptions> setupAction)
        {
            const string baseUrl = "https://api.github.com";
            
            services.AddOptions();
            services.Configure(setupAction);
            services.AddTransient<IRestClient, RestClient>(_ => new RestClient(baseUrl));
            services.AddTransient<IGitHubApiService, GitHubApiService>();
            
            return services;
        }
    }
}
