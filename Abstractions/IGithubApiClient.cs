using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ABU.GithubApiClient.Models;
using JetBrains.Annotations;

namespace ABU.GithubApiClient.Abstractions;

public interface IGithubApiClient
{
    [UsedImplicitly]
    Task<MethodResult> GetRepositoriesForUserAsync(CancellationToken ct = default);
}