using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitHubApiClient.Models;

namespace GitHubApiClient.Abstractions
{
    public interface IGitHubApiClient
    {
        Task<MethodResult<string?>> GetRepositoriesForUserAsync(CancellationToken ct = default);
    }
}