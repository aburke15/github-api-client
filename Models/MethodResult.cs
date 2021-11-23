using JetBrains.Annotations;

namespace ABU.GitHubApiClient.Models;

public class MethodResult
{
    [UsedImplicitly] public string? Json { get; set; }
    [UsedImplicitly] public string? Message { get; set; }
    [UsedImplicitly] public bool IsSuccessful { get; set; }
}