using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ABU.GithubApiClient.Abstractions;
using ABU.GithubApiClient.Constants;
using ABU.GithubApiClient.Models;
using ABU.GithubApiClient.Options;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ABU.GithubApiClient.Services;

public class GithubApiClient : IGithubApiClient
{
    private readonly IRestClient _client;
    private readonly string _username;

    public GithubApiClient(IRestClient client, IOptions<AddGithubApiClientOptions> gitHubApiClientOptions)
    {
        _client = Guard.Against.Null(client, nameof(client));
        var gitHubOptions = Guard.Against.Null(gitHubApiClientOptions.Value, nameof(gitHubApiClientOptions.Value));

        var token = Guard.Against.NullOrWhiteSpace(gitHubOptions.Token, nameof(gitHubOptions.Token));
        _username = Guard.Against.NullOrWhiteSpace(gitHubOptions.Username, nameof(gitHubOptions.Username));

        _client.BaseUrl = new Uri(GithubRoutes.BaseUrl);
        _client.Authenticator = new JwtAuthenticator(token);
    }

    public async Task<MethodResult> GetRepositoriesForUserAsync(CancellationToken ct = default)
    {
        var methodResult = new MethodResult
        {
            Json = null
        };

        var request = new RestRequest(
            string.Format(GithubRoutes.UserReposRoute, _username), Method.GET, DataFormat.Json
        ) as IRestRequest;

        var response = await _client.ExecuteGetAsync(request, ct);

        if (!response.IsSuccessful)
        {
            methodResult.Message = response.ErrorMessage;
            return methodResult;
        }

        methodResult.IsSuccessful = true;
        methodResult.Json = response.Content;

        return methodResult;
    }
}