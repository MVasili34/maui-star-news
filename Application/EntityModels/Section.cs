using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

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

    public override bool Equals(object? obj)
    {
        Section? other = obj as Section;
        if (other is null)
            return false;

        return other.SectionId == SectionId &&
            other.Name == Name &&
            other.Description == Description;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash *= 23 + SectionId.GetHashCode();
            hash *= 23 + Name.GetHashCode();
            hash *= 23 + Description.GetHashCode();
            return hash;
        }
    }
}
