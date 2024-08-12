using System.IO;
using coIT.Libraries.ConfigurationManager.Serialization;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace coIT.Libraries.ConfigurationManager
{
    public class FileSystemManager
    {
        private readonly EnvironmentManager _environmentManager;
        private readonly NewtonsoftJsonSerializer _serializer;

        public FileSystemManager()
        {
            _environmentManager = new();
            _serializer = new();
        }

        public Result<T> Get<T>()
        {
            return Result
                .Success()
                .Bind(() => _environmentManager.GetKeyNameFromObject(typeof(T)))
                .Bind(_environmentManager.GetEnvironmentValue)
                .Ensure(File.Exists, path => $"Bitte überprüfe den Pfad '{path}' auf Korrektheit")
                .Map(path => File.ReadAllText(path, System.Text.Encoding.UTF8))
                .Bind(_serializer.Deserialize<T>);
        }

        public Result<string> GetPathFor<T>()
        {
            return _environmentManager
                .GetKeyNameFromObject(typeof(T))
                .Bind(_environmentManager.GetEnvironmentValue)
                .Ensure(File.Exists, path => $"Der Pfad '{path}' ist nicht valide");
        }

        public async Task<Result> SavePathFor<T>(string path)
        {
            return await Result
                .Success()
                .Ensure(
                    () => File.Exists(path),
                    $"Bitte überprüfe den Pfad '{path}' auf Korrektheit"
                )
                .Map(() => File.ReadAllText(path, System.Text.Encoding.UTF8))
                .Check(_serializer.Deserialize<T>)
                .Bind(_ => _environmentManager.GetKeyNameFromObject(typeof(T)))
                .Bind(test => _environmentManager.SetEnvironmentValue(test, path));
        }
    }
}
