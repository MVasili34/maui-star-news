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
    public Guid? PublisherId { get; set; }

    [ForeignKey("PublisherId")]
    [InverseProperty("Articles")]
    public virtual User? Publisher { get; set; }

    [ForeignKey("SectionId")]
    [InverseProperty("Articles")]
    public virtual Section Section { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        Article? other = obj as Article;
        if (other is null)
            return false;
        
        return other.ArticleId == ArticleId &&
            other.Title == Title &&
            other.Subtitle == Subtitle &&
            other.SectionId == SectionId &&
            other.Image == Image &&
            other.Text == Text &&
            other.PublishTime == PublishTime &&
            other.PublisherId == PublisherId;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash *= 23 + ArticleId.GetHashCode();
            hash *= 23 + Title.GetHashCode();
            hash *= 23 + Subtitle.GetHashCode();
            hash *= 23 + SectionId.GetHashCode();
            hash *= 23 + Image.GetHashCode();
            hash *= 23 + Text.GetHashCode();
            hash *= 23 + PublishTime.GetHashCode();
            hash *= 23 + PublisherId.GetHashCode();
            return hash;
        }
    }
}
