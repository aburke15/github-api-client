using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ABU.GitHubApiClient.Models;
using JetBrains.Annotations;

namespace ABU.GitHubApiClient.Abstractions;

public interface IGitHubApiClient
{
    [UsedImplicitly]
    Task<MethodResult> GetRepositoriesForUserAsync(GitHubRepoRouteParams routeParams, CancellationToken ct = default);

    [UsedImplicitly]
    Task<MethodResult> GetRepositoriesForAuthUserAsync(GitHubRepoRouteParams routeParams, CancellationToken ct = default);
}