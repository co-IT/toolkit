using coIT.Libraries.ConfigurationManager.Cryptography;
using coIT.Libraries.ConfigurationManager.Serialization;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace coIT.Libraries.ConfigurationManager
{
    public class EnvironmentManager : IManageConfigurations
    {
        private readonly IDoCryptography _cryptography;
        private readonly IDoSerialization _serializer;
        private readonly EnvironmentVariableTarget _scope = EnvironmentVariableTarget.User;

        public EnvironmentManager(IDoCryptography cryptography, IDoSerialization serializer)
        {
            _cryptography = cryptography;
            _serializer = serializer;
        }

        public EnvironmentManager()
            : this(new NoCryptographyService(), new JsonSerializer()) { }

        internal Result<string> GetEnvironmentValue(string key)
        {
            return Maybe<string>
                .From(Environment.GetEnvironmentVariable(key, _scope))
                .ToResult("Konfiguration konnte nicht gefunden werden");
        }

        internal async Task<Result> SetEnvironmentValue(string key, string value)
        {
            await Task.Run(() => Environment.SetEnvironmentVariable(key, value, _scope));

            return Result.FailureIf(
                GetEnvironmentValue(key).IsFailure,
                "Konfiguration konnte nicht gespeichert werden"
            );
        }

        internal Result<string> GetKeyNameFromObject(Type type)
        {
            return Maybe<string>
                .From(type.Namespace)
                .ToResult($"Konfiguration für {type} kann nicht erstellt werden")
                .Map(GetKeyFromNamespace)
                .Map(@namespace => $"{@namespace}_{type.Name.ToUpperInvariant()}");
        }

        private string GetKeyFromNamespace(string namespaceText)
        {
            namespaceText = GetLastPartOfNamespace(namespaceText).ToUpperInvariant();
#if DEBUG
            namespaceText = $"DEBUG_{namespaceText}";
#endif
            return namespaceText;
        }

        private string GetLastPartOfNamespace(string namespaceText)
        {
            var parts = namespaceText.Split('.');

            if (parts.Length >= 2)
                return parts[^2] + "_" + parts[^1];

            return namespaceText;
        }

        public async Task<Result<T>> Get<T>()
        {
            return GetKeyNameFromObject(typeof(T))
                .Bind(GetEnvironmentValue)
                .Bind(_cryptography.Decrypt)
                .Bind(_serializer.Deserialize<T>);
        }

        public async Task<Result> Save(object konfiguration)
        {
            return await _serializer
                .Serialize(konfiguration)
                .Bind(_cryptography.Encrypt)
                .BindZip(test => GetKeyNameFromObject(konfiguration.GetType()))
                .Bind(tuple => SetEnvironmentValue(tuple.Second, tuple.First));
        }
    }
}
