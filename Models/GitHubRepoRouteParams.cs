using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ABU.GitHubApiClient.Models;

[UsedImplicitly]
public class RepositoryRouteParams
{
    private readonly List<string> _visibilityList = new List<string>
    {
        "all", "public", "private"
    };

    private int _perPage = 30;
    private string _visibility = "all";

    [JsonProperty("per_page")]
    public string PerPage
    {
        get => $"per_page={_perPage}";
        set
        {
            const int min = 30;
            const int max = 100;
            if (int.TryParse(value, out var result))
            {
                if (result > max)
                {
                    _perPage = 100;
                    return;
                }

                if (result < min)
                    return;

                _perPage = result;
            }
        }
    }

    // default: all, public, or private
    [JsonProperty("visibility")]
    public string? Visibility
    {
        get => $"visibility={_visibility}";
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            if (!_visibilityList.Contains(value))
                return;

            _visibility = value;
        }
    }

    // default: owner,collaborator,organization_member
    [JsonProperty("affiliation")]
    public string? Affiliation { get; set; }
}