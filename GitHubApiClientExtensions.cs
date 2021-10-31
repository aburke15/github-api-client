﻿using System;
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
            services = Guard.Against.Null(services, nameof(services));
            setupAction = Guard.Against.Null(setupAction, nameof(setupAction));

            services.AddOptions();
            services.Configure(setupAction);
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IGitHubApiService, GitHubApiService>();
            
            return services;
        }
    }
}
