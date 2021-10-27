using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GitHubApiClient.Models
{
    public class MethodResult<T>
    {
        [UsedImplicitly]
        public T? Result { get; set; }
        [UsedImplicitly]
        public string? Message { get; set; }
        [UsedImplicitly]
        public bool IsSuccessful { get; set; }
    }
}