using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pubs.Api.Models
{
    [Table("titles")]
    public class Title
    {
        [Key]
        [Column("title_id")]
        [StringLength(6)]
        public string TitleId { get; set; } = string.Empty;

        [Column("title")]
        [StringLength(80)]
        public string TitleName { get; set; } = string.Empty;

        [Column("type")]
        [StringLength(12)]
        public string Type { get; set; } = "UNDECIDED";

        [Column("pub_id")]
        [StringLength(4)]
        public string? PubId { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        [Column("advance", TypeName = "decimal(18,2)")]
        public decimal? Advance { get; set; }

        [Column("royalty")]
        public int? Royalty { get; set; }

        [Column("ytd_sales")]
        public int? YtdSales { get; set; }

        [Column("notes")]
        [StringLength(200)]
        public string? Notes { get; set; }

        [Column("pubdate")]
        public DateTime PubDate { get; set; } = DateTime.Now; //Obtém a data atual.

        [ForeignKey("PubId")]
        public virtual Publisher? Publisher { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = [];
    }

}
