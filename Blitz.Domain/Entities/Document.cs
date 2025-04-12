using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blitz.Domain.Entities
{
    [Table("Document")]
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public byte[] Image { get;set; }
    }
}
