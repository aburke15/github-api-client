using System;
using ABU.GithubApiClient.Abstractions;
using ABU.GithubApiClient.Options;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ABU.GithubApiClient;

[UsedImplicitly]
public static class GithubApiClientExtensions
{
    [UsedImplicitly]
    public static IServiceCollection AddGitHubApiClient(
        this IServiceCollection services,
        Action<AddGithubApiClientOptions> setupAction)
    {
        services = Guard.Against.Null(services, nameof(services));
        setupAction = Guard.Against.Null(setupAction, nameof(setupAction));

        services.AddOptions();
        services.Configure(setupAction);
        services.AddTransient<IRestClient, RestClient>();
        services.AddTransient<IGithubApiClient, Services.GithubApiClient>();

        return services;
    }
}