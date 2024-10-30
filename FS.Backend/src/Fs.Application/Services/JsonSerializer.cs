using Fs.Domain.Services;
using Newtonsoft.Json;

namespace Fs.Application.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
