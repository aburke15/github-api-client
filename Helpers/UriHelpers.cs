using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web;

namespace ABU.GitHubApiClient.Helpers;

public static class UriHelpers
{
    public static UriBuilder BuildUri(string route, NameValueCollection query)
    {
        var collection = HttpUtility.ParseQueryString(string.Empty);

        var keys = query.Cast<string>()
            .Where(s => !string.IsNullOrWhiteSpace(s));
        
        foreach (var key in keys)
            collection[key] = query[key];

        return new UriBuilder(route) { Query = collection.ToString() };
    }
}