using Microsoft.EntityFrameworkCore;

namespace EntityModels;

public partial class NewsAppDbContext : DbContext
{
    public NewsAppDbContext() 
    {
    }

    public NewsAppDbContext(DbContextOptions<NewsAppDbContext> options) 
        : base(options) 
    {
    }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=sqlserver;Server=localhost;Port=5433;Database=NewsAppDB;IntegratedSecurity=true;Pooling=true");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.Property(e => e.RoleId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("section_pkey");

            entity.Property(e => e.SectionId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("article_pkey");

            entity.Property(e => e.ArticleId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.PublishTime).HasDefaultValueSql("now()");
            entity.Property(e => e.Views).HasDefaultValue("0");

            entity.HasOne(d => d.Section).WithMany(p => p.Articles).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Publisher).WithMany(p => p.Articles).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.Property(e => e.UserId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Registered).HasDefaultValueSql("now()");
            entity.Property(e => e.LastLogin).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).OnDelete(DeleteBehavior.SetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
