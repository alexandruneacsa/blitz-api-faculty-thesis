using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blitz.Domain.Entities
{
    [Table("Borrower")]
    public class Borrower
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string cod_fiscal { get; set; }
        public string nume { get; set; }
        public string loc { get; set; }
        public string str { get; set; }
        public string nr { get; set; }
        public string di { get; set; }
        public string dp { get; set; }
        public string fax { get; set; }
        public string sect { get; set; }
        public string tel { get; set; }
        public string jud_com { get; set; }
        public string nr_com { get; set; }
        public string an_com { get; set; }
        public string act_aut { get; set; }
        public string tva { get; set; }
        public string sfarsit { get; set; }
        public string cp { get; set; }
        public string data_stare { get; set; }

        public string stare { get; set; }
        public string judet { get; set; }
        public string imp_100 { get; set; }
        public string imp_120 { get; set; }
        public string imp_200 { get; set; }
        public string imp_410 { get; set; }

        public string imp_416 { get; set; }
        public string imp_420 { get; set; }
        public string imp_423 { get; set; }
        public string imp_430 { get; set; }
        public string imp_439 { get; set; }
        public string imp_500 { get; set; }
        public string imp_602 { get; set; }
        public string imp_701 { get; set; }
        public string imp_710 { get; set; }
        public string imp_755 { get; set; }
        public string imp_756 { get; set; }
        public string bilanturi { get; set; }
        public string imp_412 { get; set; }
        public string imp_480 { get; set; }
        public string imp_432 { get; set; }
        public string detalii_adresa { get; set; }
        public string bloc { get; set; }
        public string scara { get; set; }
        public string etaj { get; set; }
        public string ap { get; set; }
    }
}
