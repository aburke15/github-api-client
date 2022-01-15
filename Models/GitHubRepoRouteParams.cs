using System.Reflection;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ABU.GitHubApiClient.Models;

[UsedImplicitly]
public class GitHubRepoRouteParams
{
    private int _perPage;

    [JsonProperty("per_page")]
    public string PerPage
    {
        get => $"per_page={_perPage}";
        set => _perPage = int.TryParse(value, out var result) ? result : 30;
    }
}