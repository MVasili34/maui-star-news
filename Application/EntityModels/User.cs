using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

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
    public int? RoleId { get; set; }

    [Column("registered", TypeName = "timestamp without time zone")]
    public DateTime Registered { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }

    public override bool Equals(object? obj)
    {
        User? other = obj as User;
        if (other is null)
            return false;

        return other.UserId == UserId &&
            other.UserName == UserName &&
            other.EmailAddress == EmailAddress &&
            other.DateOfBirth == DateOfBirth &&
            other.Phone == Phone &&
            other.PasswordSalt == PasswordSalt &&
            other.PasswordHash == PasswordHash &&
            other.RoleId == RoleId &&
            other.Registered == Registered;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash *= 23 + UserId.GetHashCode();
            hash *= 23 + UserName.GetHashCode();
            hash *= 23 + EmailAddress.GetHashCode();
            hash *= 23 + DateOfBirth.GetHashCode();
            hash *= 23 + (Phone ?? "0").GetHashCode();
            hash *= 23 + PasswordSalt.GetHashCode();
            hash *= 23 + PasswordHash.GetHashCode();
            hash *= 23 + RoleId.GetHashCode();
            hash *= 23 + Registered.GetHashCode();
            return hash;
        }
    }

}
