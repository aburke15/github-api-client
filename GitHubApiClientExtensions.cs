using System;
using Ardalis.GuardClauses;
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
            services.AddTransient<IRestClient, RestClient>(provider => new RestClient(baseUrl));
            
            services.AddTransient<IGitHubApiService, GitHubApiService>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<AddGitHubApiClientOptions>>().Value;

                Guard.Against.Null(options, nameof(options));

                var token = options.GetToken();
                Guard.Against.NullOrWhiteSpace(token, nameof(token));

                return new GitHubApiService();
            });
            
            return services;
        }
    }
}
