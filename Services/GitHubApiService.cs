using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitHubApiClient.Abstractions;
using GitHubApiClient.Models;

namespace GitHubApiClient.Services
{
    public class GitHubApiService : IGitHubApiService
    {
        public Task<IEnumerable<Repository>> GetRepositoriesForUserAsync(CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }
    }
}