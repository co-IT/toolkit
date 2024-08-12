using System.Text;
using System.Text.RegularExpressions;
using coIT.Libraries.Gdi.Accounting.Contracts;
using CSharpFunctionalExtensions;

namespace coIT.Libraries.Gdi.Accounting
{
    public class UmsatzParser
    {
        public static async Task<Result<IEnumerable<SaleBooking>>> ParseCsv(string pfad)
        {
            if (!File.Exists(pfad))
            {
                var fehler = "Csv Datei konnte nicht gefunden werden";
                return Result.Failure<IEnumerable<SaleBooking>>(fehler);
            }

            var csvZeilen = await File.ReadAllLinesAsync(pfad, Encoding.Default);
            var kontoBlöcke = KontoBlöckeErhalten(csvZeilen.ToList()).ToList();
            var kontoBlöckeOhneDopplungen = KontoBlöckeKombinieren(kontoBlöcke);
            var buchungsZeilen = kontoBlöcke.SelectMany(kontoMitZeilen =>
                kontoMitZeilen.Zeilen.Select(zeile =>
                    RechnungszeileParsen(kontoMitZeilen.Konto, zeile)
                )
            );
            var buchungsZeilenOhneDoppelteRechnungen = DuplettenKombinieren(buchungsZeilen);

            return Result.Success(buchungsZeilenOhneDoppelteRechnungen);
        }

        private static IEnumerable<SaleBooking> DuplettenKombinieren(
            IEnumerable<SaleBooking> buchungsZeilen
        )
        {
            return buchungsZeilen
                .GroupBy(zeile => zeile.InvoiceNumber)
                .Select(gruppe =>
                {
                    var ersteZeile = gruppe.First();
                    var nettoSumme = gruppe.Sum(zeile => zeile.NetValue);
                    return new SaleBooking
                    {
                        InvoiceNumber = ersteZeile.InvoiceNumber,
                        AccountNumber = ersteZeile.AccountNumber,
                        CustomerName = ersteZeile.CustomerName,
                        CustomerNumber = ersteZeile.CustomerNumber,
                        NetValue = nettoSumme,
                        Date = ersteZeile.Date
                    };
                });
        }

        private static Dictionary<int, List<string>> KontoBlöckeKombinieren(
            IEnumerable<(int Konto, List<string> Zeilen)> blöcke
        )
        {
            return blöcke
                .GroupBy(x => x.Konto)
                .ToDictionary(g => g.Key, g => g.SelectMany(x => x.Zeilen).ToList());
        }

        private static IEnumerable<(int Konto, List<string> Zeilen)> KontoBlöckeErhalten(
            List<string> csvZeilen
        )
        {
            for (
                var derzeitigeZeilenNummer = 0;
                derzeitigeZeilenNummer < csvZeilen.Count;
                derzeitigeZeilenNummer++
            )
            {
                var derzeitigeZeile = csvZeilen[derzeitigeZeilenNummer];

                var maybeNächsteKontonummer = ZeileIstBeginnVonKontoBlock(derzeitigeZeile);
                if (maybeNächsteKontonummer.HasNoValue)
                    continue;

                var nächsteKontoNummer = maybeNächsteKontonummer.Value;
                derzeitigeZeilenNummer += 2;

                var restlicherText = csvZeilen.GetRange(
                    derzeitigeZeilenNummer,
                    csvZeilen.Count - derzeitigeZeilenNummer
                );

                var längeDesBuchungsBlocks = LängeDesNächstenBuchungsblocksErhalten(restlicherText);
                var buchungsBlock = csvZeilen.GetRange(
                    derzeitigeZeilenNummer,
                    längeDesBuchungsBlocks
                );

                yield return new(nächsteKontoNummer, buchungsBlock);

                derzeitigeZeilenNummer += längeDesBuchungsBlocks;
            }
        }

        private static Maybe<int> ZeileIstBeginnVonKontoBlock(string zeile)
        {
            var kontoAmAnfangVonZeileRegex = new Regex(@"^\d{4}\.\d{2}");
            if (!kontoAmAnfangVonZeileRegex.IsMatch(zeile))
                return Maybe.None;

            var kontoNummerMitKommaUndNullen = kontoAmAnfangVonZeileRegex.Match(zeile).Value;
            var kontoOhneKommaUndNullen = kontoNummerMitKommaUndNullen.Substring(
                0,
                kontoNummerMitKommaUndNullen.Length - 3
            );
            return int.Parse(kontoOhneKommaUndNullen);
        }

        private static int LängeDesNächstenBuchungsblocksErhalten(List<string> restlicherText)
        {
            var buchungStartChars = new List<char> { 'B', 'G', 'R' };

            for (
                var derzeitigeZeilenNummer = 0;
                derzeitigeZeilenNummer < restlicherText.Count;
                derzeitigeZeilenNummer++
            )
            {
                var derzeitigeZeile = restlicherText[derzeitigeZeilenNummer];

                if (derzeitigeZeile.Length < 1)
                    return derzeitigeZeilenNummer;

                var ersterBuchstabe = derzeitigeZeile[0];

                if (!buchungStartChars.Contains(ersterBuchstabe))
                    return derzeitigeZeilenNummer;
            }

            return restlicherText.Count;
        }

        private static SaleBooking RechnungszeileParsen(int konto, string zeile)
        {
            var felder = zeile.Split(';');

            var nettoFeld = string.IsNullOrWhiteSpace(felder[29]) ? felder[28] : felder[29];
            if (string.IsNullOrWhiteSpace(nettoFeld))
                nettoFeld = "0";

            return new SaleBooking()
            {
                InvoiceNumber = felder[2],
                AccountNumber = konto,
                CustomerName = felder[17],
                CustomerNumber = int.Parse(felder[10]),
                NetValue = decimal.Parse(nettoFeld),
                Date = DateOnly.Parse(felder[5])
            };
        }
    }
}
