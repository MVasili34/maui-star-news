using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityModels;

[Table("section")]
public partial class Section
{
    [Key]
    [Column("section_id")]
    public int SectionId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("material_icon")]
    [StringLength(10)]
    public string? MaterialIcon { get; set; }

    [Column("description")]
    [StringLength(100)]
    public string Description { get; set; } = null!;

    [InverseProperty("Section")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
