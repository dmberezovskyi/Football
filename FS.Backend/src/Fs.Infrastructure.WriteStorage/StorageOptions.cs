using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.Storage
{
    public sealed class StorageOptions : IOptions<StorageOptions>
    {
        public string ConnectionString { get; set; }

        StorageOptions IOptions<StorageOptions>.Value => this;
    }
}
