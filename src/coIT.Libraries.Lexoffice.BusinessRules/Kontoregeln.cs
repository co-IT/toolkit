namespace coIT.Libraries.Lexoffice.BusinessRules
{
    internal class Kontoregeln
    {
        public List<int> ErlaubteKontenFürKunden(int debitorNummer)
        {
            return debitorNummer switch
            {
                50000 => [8504, 8505], // heco
                50001 => [8504, 8505], // hecoform
                50039 => [8337], // Varinoc
                _ => [8500, 8506]
            };
        }
    }
}
