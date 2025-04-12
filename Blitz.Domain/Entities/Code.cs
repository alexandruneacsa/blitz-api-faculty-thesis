using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blitz.Domain.Entities
{
    [Table("Codes")]
    public class Code
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nume { get; set; }
        public string CodFiscal { get; set; }
        public string NumarRegistrulComertului { get; set; }
        public string Tara { get; set; }
        public string Judet { get; set; }
        public string Localitate { get; set; }
        public string Sector { get; set; }
        public string Strada { get; set; }
        public string Numar { get; set; }
        public string Bloc { get; set; }
        public string Etaj { get; set; }
        public string Apartament { get; set; }
        public string Camera { get; set; }
        public string Telefon { get; set; }

        public string StareFirma { get; set; }
        public string Salariati2021 { get; set; }
        public string CifraDeAfaceriNetaRON2021 { get; set; }
        public string ProfitNetRON2021 { get; set; }
        public string CotaDePiata2021 { get; set; }
        public string CodCAEN2021 { get; set; }
        public string DescriereCodCAEN2021 { get; set; }
        public string Salariati2020 { get; set; }
        public string CifraDeAfaceriNetaRON2020 { get; set; }
        public string ProfitNetRON2020 { get; set; }
        public string CotaDePiata2020 { get; set; }
        public string CodCAEN2020 { get; set; }
        public string DescriereCodCAEN2020 { get; set; }
        public string Salariati2019 { get; set; }
        public string CifraDeAfaceriNetaRON2019 { get; set; }
        public string ProfitNetRON2019 { get; set; }
        public string CotaDePiata2019 { get; set; }
        public string CodCAEN2019 { get; set; }
        public string DescriereCodCAEN2019 { get; set; }
        public string Salariati2018 { get; set; }
        public string CifraDeAfaceriNetaRON2018 { get; set; }
        public string ProfitNetRON2018 { get; set; }
        public string CotaDePiata2018 { get; set; }
        public string CodCAEN2018 { get; set; }
        public string DescriereCodCAEN2018 { get; set; }
    }
}