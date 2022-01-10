using System.Threading;

namespace ABU.GitHubApiClient.Constants;

internal static class GitHubRoutes
{
    internal const string BaseUrl = "https://api.github.com";

    /// <summary>
    /// Route for getting the user's repos. Placeholder should be replaced with the GitHub username.
    /// </summary>
    internal const string UserReposRoute = "/users/{0}/repos";
}