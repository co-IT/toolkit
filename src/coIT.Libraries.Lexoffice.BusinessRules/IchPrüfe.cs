using CSharpFunctionalExtensions;

namespace coIT.Libraries.Lexoffice.BusinessRules
{
    public interface IchPrüfe<T>
    {
        public Result Prüfen(T rechnung);
    }
}
