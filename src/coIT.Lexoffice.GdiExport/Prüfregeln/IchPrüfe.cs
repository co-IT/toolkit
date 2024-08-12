using CSharpFunctionalExtensions;

namespace coIT.Lexoffice.GdiExport.Prüfregeln
{
    internal interface IchPrüfe<T>
    {
        public Result Prüfen(T rechnung);
    }
}
