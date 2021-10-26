using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GitHubApiClient.Abstractions;
using GitHubApiClient.Models;
using GitHubApiClient.Options;
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
        
        public async Task<IEnumerable<Repository>> GetRepositoriesForUserAsync(CancellationToken ct = default)
        {
            _client.Authenticator = new JwtAuthenticator(_token);
            // TODO: class containing all of the routes as static strings
            var request = new RestRequest(
                $"/users/{_username}/repos", Method.GET, DataFormat.Json
            ) as IRestRequest;

            var response = await _client.ExecuteGetAsync(request, ct);

            return !response.IsSuccessful ? Enumerable.Empty<Repository>() 
                : JsonConvert.DeserializeObject<IEnumerable<Repository>>(response.Content)!;
        }
    }
}