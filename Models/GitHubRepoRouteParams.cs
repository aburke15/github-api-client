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
        set
        {
            if (int.TryParse(value, out var result))
                result = result > 100 ? 100 : result;
            else
                result = 30;

            _perPage = result;
        }
    }
}