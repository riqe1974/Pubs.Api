using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pubs.Api.Models
{
    [Table("sales")]
    public class Sale
    {
        [Key]
        [Column("stor_id")]
        [StringLength(4)]
        public string StorId { get; set; } = string.Empty;

        [Key]
        [Column("ord_num")]
        [StringLength(20)]
        public string OrdNum { get; set; } = string.Empty;

        [Column("ord_date")]
        public DateTime OrdDate { get; set; }

        [Column("qty")]
        public short Qty { get; set; }

        [Column("payterms")]
        [StringLength(12)]
        public string Payterms { get; set; } = string.Empty;

        [Column("title_id")]
        [StringLength(6)]
        public string TitleId { get; set; } = string.Empty;

        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; } = null!;
    }
}
