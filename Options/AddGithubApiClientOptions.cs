using JetBrains.Annotations;

namespace ABU.GithubApiClient.Options;

[UsedImplicitly]
public class AddGithubApiClientOptions
{
    public string? Token { get; private set; }
    public string? Username { get; private set; }

    /// <summary>
    /// Sets the GitHub personal access token for completing the API calls.
    /// </summary>
    /// <param name="token"></param>
    public void AddToken(string token) => Token = token;

    /// <summary>
    /// Sets the GitHub username that is associated with the personal access token.
    /// </summary>
    /// <param name="username"></param>
    public void AddUsername(string username) => Username = username;
}