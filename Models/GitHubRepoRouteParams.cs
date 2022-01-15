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
        get => $"{GetType().GetCustomAttribute<JsonPropertyAttribute>()!.PropertyName}={_perPage}";
        set => _perPage = int.TryParse(value, out var num) ? num : 30;
    }
}