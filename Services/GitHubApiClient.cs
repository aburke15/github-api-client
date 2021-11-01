using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GitHubApiClient.Abstractions;
using GitHubApiClient.Constants;
using GitHubApiClient.Models;
using GitHubApiClient.Options;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace GitHubApiClient.Services
{
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
            
            _client.BaseUrl = new Uri(GitHubRoutes.BaseUrl);
            _client.Authenticator = new JwtAuthenticator(token);
        }
        
        public async Task<MethodResult<string?>> GetRepositoriesForUserAsync(CancellationToken ct = default)
        {
            var methodResult = new MethodResult<string?>()
            {
                Result = null
            };
            
            var request = new RestRequest(
                string.Format(GitHubRoutes.UserReposRoute, _username), Method.GET, DataFormat.Json
            ) as IRestRequest;

            var response = await _client.ExecuteGetAsync(request, ct);

            if (!response.IsSuccessful)
            {
                methodResult.Message = response.ErrorMessage;
                return methodResult;
            }

            methodResult.IsSuccessful = true;
            methodResult.Result = JsonConvert.SerializeObject(response.Content, Formatting.Indented);

            return methodResult;
        }
    }
}