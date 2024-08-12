using CSharpFunctionalExtensions;

namespace coIT.Libraries.ConfigurationManager.Serialization
{
    public interface IDoSerialization
    {
        public Result<string> Serialize(object obj);

        public Result<T> Deserialize<T>(string serializedObject);
    }
}
