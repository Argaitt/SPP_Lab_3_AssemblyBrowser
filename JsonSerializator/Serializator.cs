using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AssemblyScanner;

namespace JsonSerializator
{
    public class Serializator
    {
        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj); ;
        }
        public object Deserialize(string jsonString)
        {
            
            return JsonSerializer.Deserialize<InfoCell>(jsonString);
        }
    }
}
