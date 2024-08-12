using System.Text.Json;
using CSharpFunctionalExtensions;
using CsharpSerializer = System.Text.Json.JsonSerializer;

namespace coIT.Libraries.ConfigurationManager.Serialization
{
    public class JsonSerializer : IDoSerialization
    {
        public Result<T> Deserialize<T>(string serializedObject)
        {
            try
            {
                var deserializedObject = CsharpSerializer.Deserialize<T>(serializedObject);
                return Maybe
                    .From(CsharpSerializer.Deserialize<T>(serializedObject))
                    .ToResult("Konfiguration konnte nicht geladen werden")!;
            }
            catch (JsonException ex)
            {
                return Result.Failure<T>("Fehler beim Laden der Konfiguration");
            }
        }

        public Result<string> Serialize(object obj) => CsharpSerializer.Serialize(obj);
    }
}
