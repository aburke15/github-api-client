using System.Security.Cryptography;
using JetBrains.Annotations;

namespace ABU.GitHubApiClient.Models;

public record MethodResult
{
    [UsedImplicitly] public string? Json { get; set; }
    [UsedImplicitly] public string? Message { get; set; }
    [UsedImplicitly] public bool IsSuccessful { get; private set; }

    public void SetIsSuccessfulFalse() => IsSuccessful = false;
    public void SetIsSuccessfulTrue() => IsSuccessful = true;
}