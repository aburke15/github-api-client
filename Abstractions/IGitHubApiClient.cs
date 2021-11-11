using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GitHubApiClient.Models;
using JetBrains.Annotations;

namespace GitHubApiClient.Abstractions;

public interface IGitHubApiClient
{
    [UsedImplicitly]
    Task<MethodResult> GetRepositoriesForUserAsync(CancellationToken ct = default);
}