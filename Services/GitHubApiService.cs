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
    public class GitHubApiService : IGitHubApiService
    {
        private readonly IRestClient _client;
        private readonly string _token;
        private readonly string _username;
        
        public GitHubApiService(IRestClient client, IOptions<AddGitHubApiClientOptions> gitHubApiClientOptions)
        {
            _client = Guard.Against.Null(client, nameof(client));
            var gitHubOptions = Guard.Against.Null(gitHubApiClientOptions.Value, nameof(gitHubApiClientOptions.Value));

            _token = Guard.Against.NullOrWhiteSpace(gitHubOptions.Token, nameof(gitHubOptions.Token));
            _username = Guard.Against.NullOrWhiteSpace(gitHubOptions.Username, nameof(gitHubOptions.Username));
        }
        
        public async Task<MethodResult<IEnumerable<Repository>>> GetRepositoriesForUserAsync(CancellationToken ct = default)
        {
            var methodResult = new MethodResult<IEnumerable<Repository>>()
            {
                Result = Enumerable.Empty<Repository>()
            };
            
            _client.Authenticator = new JwtAuthenticator(_token);
            // TODO: class containing all of the routes as static strings
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
            methodResult.Result = JsonConvert.DeserializeObject<IEnumerable<Repository>>(response.Content);

            return methodResult;
        }
    }
}