using Blitz.Application.Dtos;
using Blitz.Application.Interfaces;
using Blitz.Domain.Entities;
using Blitz.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Blitz.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ICode _code;
        private readonly IBorrower _borrower;

        public ReportService(ICode code, IBorrower borrower)
        {
            _code = code;
            _borrower = borrower;
        }

        public async Task<BlitzWrapper<List<Borrower>>> LoadBorrowersAsync(List<IFormFile> formFiles,
            CancellationToken cancellationToken)
        {
            var listOfBorrowers = new List<Borrower>();

            if (formFiles == null || !formFiles.Any())
            {
                return new BlitzWrapper<List<Borrower>>("The system needs at list one document loaded", null, 404);
            }

            await Parallel.ForEachAsync(formFiles, async (file, ca) =>
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream, ca);

                stream.Seek(0, SeekOrigin.Begin);

                List<string> fileContents;
                using StreamReader reader = new(stream);

                fileContents = (await reader.ReadToEndAsync()).Replace("\\,", ".").Replace("\r", "").Split("\n")
                    .ToList();

                var length = fileContents[0].Split(",").Length;

                fileContents.RemoveAt(0);

                foreach (var line in fileContents)
                {
                    var parts = line.Split(",");

                    if (parts.Length != length)
                    {
                        continue;
                    }

                    var borrowerView = new Borrower
                    {
                        cod_fiscal = parts[0],
                        nume = parts[1],
                        loc = parts[2],
                        str = parts[3],
                        nr = parts[4],
                        di = parts[5],
                        dp = parts[6],
                        fax = parts[7],
                        sect = parts[8],
                        tel = parts[9],
                        jud_com = parts[10],
                        nr_com = parts[11],
                        an_com = parts[12],
                        act_aut = parts[13],
                        tva = parts[14],
                        sfarsit = parts[15],
                        cp = parts[16],
                        data_stare = parts[17],
                        stare = parts[18],
                        judet = parts[19],
                        imp_100 = parts[20],
                        imp_120 = parts[21],
                        imp_200 = parts[22],
                        imp_410 = parts[23],
                        imp_416 = parts[24],
                        imp_420 = parts[25],
                        imp_423 = parts[26],
                        imp_430 = parts[27],
                        imp_439 = parts[28],
                        imp_500 = parts[29],
                        imp_602 = parts[30],
                        imp_701 = parts[31],
                        imp_710 = parts[32],
                        imp_755 = parts[33],
                        bilanturi = parts[34],
                        imp_412 = parts[35],
                        imp_480 = parts[36],
                        imp_432 = parts[37],
                        detalii_adresa = parts[38],
                        bloc = parts[39],
                        scara = parts[40],
                        etaj = parts[41],
                        ap = parts[42],
                    };

                    listOfBorrowers.Add(borrowerView);
                }
            });

            await _borrower.AddMultipleBorrowersAsync(listOfBorrowers);

            return new BlitzWrapper<List<Borrower>>("Result", listOfBorrowers, 200);
        } 

        /*
         * Optimized
         */
        public async Task LoadCodesAsync(List<IFormFile> formFiles)
        {
            var listOfCodes = new List<Code>();

            await Parallel.ForEachAsync(formFiles, async (file, ca) =>
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream, ca);

                stream.Seek(0, SeekOrigin.Begin);

                List<string> fileContents;
                using StreamReader reader = new(stream);

                fileContents = (await reader.ReadToEndAsync()).Replace("\\,", ".").Replace("\r", "").Split("\n")
                    .ToList();

                var length = fileContents[0].Split(",").Length;

                fileContents.RemoveAt(0);

                foreach (var line in fileContents)
                {
                    var parts = line.Split(",");

                    if (parts.Length != length)
                    {
                        continue;
                    }

                    var codeView = new Code
                    {
                        Nume = parts[0],
                        CodFiscal = parts[1],
                        NumarRegistrulComertului = parts[2],
                        Tara = parts[3],
                        Judet = parts[4],
                        Localitate = parts[5],
                        Sector = parts[6],
                        Strada = parts[7],
                        Numar = parts[8],
                        Bloc = parts[9],
                        Etaj = parts[10],
                        Apartament = parts[11],
                        Camera = parts[12],
                        Telefon = parts[13],
                        StareFirma = parts[14],
                        Salariati2021 = parts[15],
                        CifraDeAfaceriNetaRON2021 = parts[16],
                        ProfitNetRON2021 = parts[17],
                        CotaDePiata2021 = parts[18],
                        CodCAEN2021 = parts[19],
                        DescriereCodCAEN2021 = parts[20],
                        Salariati2019 = parts[21],
                        CifraDeAfaceriNetaRON2019 = parts[22],
                        ProfitNetRON2019 = parts[23],
                        CotaDePiata2019 = parts[24],
                        CodCAEN2019 = parts[25],
                        DescriereCodCAEN2019 = parts[26],
                        Salariati2020 = parts[27],
                        CifraDeAfaceriNetaRON2020 = parts[28],
                        ProfitNetRON2020 = parts[29],
                        CotaDePiata2020 = parts[30],
                        CodCAEN2020 = parts[31],
                        DescriereCodCAEN2020 = parts[32],
                        Salariati2018 = parts[33],
                        CifraDeAfaceriNetaRON2018 = parts[34],
                        ProfitNetRON2018 = parts[35],
                        CotaDePiata2018 = parts[36],
                        CodCAEN2018 = parts[37],
                        DescriereCodCAEN2018 = parts[38],
                    };

                    listOfCodes.Add(codeView);
                }
            });

            await _code.AddCodesAsync2(listOfCodes);
        } 
    }
}