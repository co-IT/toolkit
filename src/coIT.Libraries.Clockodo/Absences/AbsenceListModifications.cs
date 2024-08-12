using coIT.Libraries.Clockodo.Absences.Contracts;

namespace coIT.Libraries.Clockodo.Absences;

internal static class AbsenceListModifications
{
    internal static IEnumerable<Absence> ApplyElectronicalCertificateOfDisability(
        this IEnumerable<Absence> absences
    )
    {
        foreach (var absence in absences)
        {
            if (absence.HasElectronicalCertificateOfDisability is true && absence.TypeId == 4)
            {
                absence.TypeId = -1;
            }

            if (absence.HasElectronicalCertificateOfDisability is true && absence.TypeId == 5)
            {
                absence.TypeId = -2;
            }
            yield return absence;
        }
    }

    internal static IEnumerable<Absence> FilterByPeriod(
        this IEnumerable<Absence> absences,
        ClockodoPeriodFilter filter
    )
    {
        foreach (var absence in absences)
        {
            var periodOfAbsence = new DateSpan(absence.Start, absence.End);
            if (periodOfAbsence.IntersectsWith(filter.Period))
                yield return absence;
        }
    }

    internal static IEnumerable<Absence> SplitAbsencesByMonth(
        this IEnumerable<Absence> absences,
        List<ClockodoAbsenceType> absenceTypes
    )
    {
        foreach (var absence in absences)
        {
            var absenceType =
                absenceTypes.FirstOrDefault(t => t.Id == absence.TypeId)
                ?? throw new InvalidDataException(
                    $"Abwesenheitstyp {absence.TypeId} ist unbekannt."
                );

            var period = new DateSpan(absence.Start, absence.End);
            var splittedTimeSpans = period.SplitAtMonthEnd();

            foreach (var timespan in splittedTimeSpans)
                yield return new Absence
                {
                    Id = absence.Id,
                    UserId = absence.UserId,
                    Start = timespan.Start,
                    End = timespan.End,
                    Status = absence.Status,
                    TypeId = absence.TypeId,
                    AbsenceType = absenceType,
                    Duration = absence.Duration
                };
        }
    }
}
