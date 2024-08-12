using System.Text;
using coIT.Database.Entities;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace coIT.Database.Utilities;

internal static class ResultExtensions
{
    public static string ContentToString(this IEnumerable<Result> list)
    {
        var sb = new StringBuilder();

        foreach (var result in list)
            if (result.IsFailure)
                sb.AppendLine(result.Error);

        return sb.ToString();
    }

    public static ClockodoFailure UnwrapClockodoError(this Result failure)
    {
        if (failure.IsSuccess)
            throw new ArgumentOutOfRangeException(
                nameof(failure),
                "Es können nur Fehler verarbeitet werden."
            );

        var obj = JsonConvert.DeserializeObject<ClockodoFailure>(failure.Error);

        return obj;
    }
}
