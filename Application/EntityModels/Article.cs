using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityModels;

[Table("article")]
public partial class Article
{
    [Key]
    [Column("article_id")]
    public Guid ArticleId { get; set; }

    [Column("title")]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column("subtitle")]
    [StringLength(100)]
    public string Subtitle { get; set; } = null!;

    [Column("section_id")]
    public int SectionId { get; set; }

    [Column("image", TypeName = "character varying")]
    public string Image { get; set; } = null!;

    [Column("text")]
    public string Text { get; set; } = null!;

    [Column("publish_time", TypeName = "timestamp without time zone")]
    public DateTime PublishTime { get; set; }

    [Column("publisher_id")]
    public Guid PublisherId { get; set; }

    [ForeignKey("PublisherId")]
    [InverseProperty("Articles")]
    public virtual User? Publisher { get; set; }

    [ForeignKey("SectionId")]
    [InverseProperty("Articles")]
    public virtual Section Section { get; set; } = null!;
}
