using System.Collections.Specialized;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ABU.GitHubApiClient.Models;

[UsedImplicitly]
public class RepositoryRouteParams
{
    private int _perPage;
    private string? _visibility = "all";
    private string? _affiliation = "owner,collaborator,organization_member";
    private int _page;

    private readonly List<string> _visibilityList = new() { "all", "public", "private" };

    // default: 30
    // options 30 to 100
    [JsonProperty("per_page")]
    public string PerPage
    {
        get => $"per_page={_perPage}";
        set
        {
            const int min = 30;
            const int max = 100;

            if (!int.TryParse(value, out var result)) return;

            switch (result)
            {
                case > max:
                    _perPage = max;
                    return;
                case < min:
                    _perPage = min;
                    return;
                default:
                    _perPage = result;
                    return;
            }
        }
    }

    // default: all
    // options all, public, private
    [JsonProperty("visibility")]
    public string Visibility
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
    // options owner, collaborator, organization_member
    [JsonProperty("affiliation")]
    public string Affiliation
    {
        get => $"affiliation={_affiliation}";
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            _affiliation = value;
        }
    }

    // default: 1
    [JsonProperty("page")]
    public string Page
    {
        get => $"page={_page}";
        set
        {
            const int min = 1;

            if (!int.TryParse(value, out var result)) return;

            switch (result)
            {
                case < min:
                    _page = min;
                    return;
                default:
                    _page = result;
                    return;
            }
        }
    }

    public NameValueCollection GatherRouteParams()
    {
        var routeParams = new NameValueCollection();
        
        const char separator = '=';
        
        var perPage = PerPage.Split(separator);
        var visibility = Visibility.Split(separator);
        var affiliation = Affiliation.Split(separator);
        var page = Page.Split(separator);
        
        routeParams.Add(perPage[0], perPage[1]);
        routeParams.Add(visibility[0], visibility[1]);
        routeParams.Add(affiliation[0], affiliation[1]);
        routeParams.Add(page[0], page[1]);

        return routeParams;
    }
}