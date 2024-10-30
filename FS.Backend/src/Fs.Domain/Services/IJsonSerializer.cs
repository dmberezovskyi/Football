using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Domain.Services
{
    public interface IJsonSerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string json);
    }
}
