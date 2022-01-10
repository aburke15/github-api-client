using System.Threading;

namespace ABU.GithubApiClient.Constants;

internal static class GithubRoutes
{
    internal const string BaseUrl = "https://api.github.com";

    /// <summary>
    /// Route for getting the user's repos. Placeholder should be replaced with the GitHub username.
    /// </summary>
    internal const string UserReposRoute = "/users/{0}/repos";
}