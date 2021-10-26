using JetBrains.Annotations;

namespace GitHubApiClient.Options
{
    [UsedImplicitly]
    public class AddGitHubApiClientOptions
    {
        private string? _token;
        public void AddToken(string token) => _token = token;
        public string? GetToken() => _token;
    }
}