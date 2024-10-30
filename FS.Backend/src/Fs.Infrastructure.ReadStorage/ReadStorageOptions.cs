using Microsoft.Extensions.Options;

namespace Fs.Infrastructure.ReadStorage
{
    public sealed class ReadStorageOptions : IOptions<ReadStorageOptions>
    {
        public string ConnectionString { get; set; }

        ReadStorageOptions IOptions<ReadStorageOptions>.Value => this;
    }
}
