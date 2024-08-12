using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace coIT.Libraries.ConfigurationManager.Serialization
{
    public class NewtonsoftJsonSerializer : IDoSerialization
    {
        public Result<T> Deserialize<T>(string serializedObject)
        {
            try
            {
                return Maybe
                    .From(JsonConvert.DeserializeObject<T>(serializedObject))
                    .ToResult("Konfiguration konnte nicht geladen werden")!;
            }
            catch (JsonException ex)
            {
                return Result.Failure<T>("Fehler beim Laden der Konfiguration");
            }
        }

        public Result<string> Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}
