using System;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubApiClient
{
    public static class GitHubApiClientExtensions
    {
        public static IServiceCollection AddGitHubApiClient(this IServiceCollection services)
        {
            return services;
        }
    }
}
