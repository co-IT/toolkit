using CSharpFunctionalExtensions;

namespace coIT.Libraries.ConfigurationManager
{
    public interface IManageConfigurations
    {
        public Task<Result<T>> Get<T>();
        public Task<Result> Save(object konfiguration);
    }
}
