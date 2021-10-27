using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitHubApiClient.Models;

namespace GitHubApiClient.Abstractions
{
    public interface IGitHubApiService
    {
        Task<MethodResult<IEnumerable<Repository>>> GetRepositoriesForUserAsync(CancellationToken ct = default);
    }
}