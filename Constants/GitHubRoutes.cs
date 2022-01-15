using System.Threading;

namespace ABU.GitHubApiClient.Constants;

internal static class GitHubRoutes
{
    internal const string BaseUrl = "https://api.github.com";

    /// <summary>
    /// Route for getting the user's repos, default page size is 30. Placeholder should be replaced with the GitHub username.
    /// </summary>
    internal const string PublicReposRoute = "/users/{0}/repos";
    /// <summary>
    /// Route for getting the user's repos if authenticated, default page size is 30.
    /// </summary>
    internal const string PrivateReposRoute = "/users/repos";
}