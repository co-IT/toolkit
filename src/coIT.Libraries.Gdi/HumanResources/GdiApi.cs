namespace coIT.Libraries.Gdi.HumanResources;

public class GdiApi
{
    private readonly GdiAbsenceType _type;

    public GdiApi(GdiAbsenceType type)
    {
        _type = type;
    }

    public GdiEntry SetPersonalnr(int personnelNo)
    {
        return new GdiEntry(personnelNo, _type);
    }
}
