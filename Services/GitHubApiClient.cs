using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ABU.GitHubApiClient.Abstractions;
using ABU.GitHubApiClient.Constants;
using ABU.GitHubApiClient.Models;
using ABU.GitHubApiClient.Options;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ABU.GitHubApiClient.Services;

public class GitHubApiClient : IGitHubApiClient
{
    private readonly IRestClient _client;
    private readonly string _username;

    public GitHubApiClient(IRestClient client, IOptions<AddGitHubApiClientOptions> gitHubApiClientOptions)
    {
        _client = Guard.Against.Null(client, nameof(client));
        var gitHubOptions = Guard.Against.Null(gitHubApiClientOptions.Value, nameof(gitHubApiClientOptions.Value));

        var token = Guard.Against.NullOrWhiteSpace(gitHubOptions.Token, nameof(gitHubOptions.Token));
        _username = Guard.Against.NullOrWhiteSpace(gitHubOptions.Username, nameof(gitHubOptions.Username));
        
        _client.Authenticator = new JwtAuthenticator(token);
    }

    public async Task<MethodResult> GetRepositoriesForUserAsync(GitHubRepoRouteParams routeParams, CancellationToken ct = default)
    {
        var builder = new UriBuilder($"{GitHubRoutes.BaseUrl}{string.Format(GitHubRoutes.PublicReposRoute, _username)}")
        {
            Query = routeParams.PerPage
        };
        
        return await GetRepositoriesAsync(builder.ToString(), ct);
    }

    public async Task<MethodResult> GetRepositoriesForAuthUserAsync(GitHubRepoRouteParams routeParams, CancellationToken ct = default)
    {
        var builder = new UriBuilder($"{GitHubRoutes.BaseUrl}{GitHubRoutes.PrivateReposRoute}")
        {
            Query = routeParams.PerPage
        };
        
        return await GetRepositoriesAsync(builder.ToString(), ct);
    }

    private async Task<MethodResult> GetRepositoriesAsync(string resource, CancellationToken ct = default)
    {
        var result = new MethodResult();

        var request = new RestRequest(
            resource, Method.GET, DataFormat.Json
        ) as IRestRequest;

        var response = await _client.ExecuteGetAsync(request, ct);

        if (!response.IsSuccessful)
        {
            result.Message = response.ErrorMessage;
            result.SetIsSuccessfulFalse();
            
            return result;
        }

        result.SetIsSuccessfulTrue();
        result.Json = response.Content;

        return result;
    }
}