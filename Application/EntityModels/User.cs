using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityModels;

[Table("user")]
public partial class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("user_name")]
    [StringLength(25)]
    public string UserName { get; set; } = null!;

    [Column("email_address", TypeName = "character varying")]
    public string EmailAddress { get; set; } = null!;

    [Column("phone")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("password_salt", TypeName = "character varying")]
    public string PasswordSalt { get; set; } = null!;

    [Column("password_hash", TypeName = "character varying")]
    public string PasswordHash { get; set; } = null!;

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("registered", TypeName = "timestamp without time zone")]
    public DateTime Registered { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

}
