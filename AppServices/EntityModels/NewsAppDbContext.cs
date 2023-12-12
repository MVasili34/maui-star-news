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

        modelBuilder.Entity<Role>()
            .HasData(new Role {
                RoleId = 1,
                Name = "Читатель",
                Description = "Обычный читатель с базовыми правами доступа",
            },
                    new Role {
                RoleId = 2,
                Name = "Редактор",
                Description = "Редактор с правами публикации статей"
            }, 
                    new Role {
                RoleId = 3,
                Name = "Администратор",
                Description = "Полный доступ ко всем возможностям и статистике приложения"
            });

        modelBuilder.Entity<Section>()
            .HasData(new Section {
                SectionId = 1,
                Name = "Политика",
                MaterialIcon = "\ue84f",
                Description = "Новости о политике"
            }, new Section
            {
                SectionId = 2,
                Name = "История",
                MaterialIcon = "\ue153",
                Description = "Сводки о прошлом, настоящем и будущем"
            }, new Section
            {
                SectionId = 3,
                Name = "Образование",
                MaterialIcon = "\ue80c",
                Description = "Новости о нововведениях в системе образования"
            },  new Section
            {
                SectionId = 4,
                Name = "Бизнес",
                MaterialIcon = "\ue8f9",
                Description = "Новости о предпринимательстве"
            }, new Section
            {
                SectionId = 5,
                Name = "Маркетинг",
                MaterialIcon = "\ue8d1",
                Description = "Новости об управлении"
            }, new Section
            {
                SectionId = 6,
                Name = "Природа",
                MaterialIcon = "\ue545",
                Description = "Новости о природе"
            }, new Section
            {
                SectionId = 7,
                Name = "Здоровье",
                MaterialIcon = "\ueb4c",
                Description = "Нововведения в медицине"
            }, new Section
            {
                SectionId = 8,
                Name = "Спорт",
                MaterialIcon = "\ue921",
                Description = "Новости о соревнованиях"
            }, new Section
            {
                SectionId = 9,
                Name = "Кино",
                MaterialIcon = "\ue02c",
                Description = "Новые премьеры"
            }, new Section
            {
                SectionId = 10,
                Name = "Наука",
                MaterialIcon = "\uea4b",
                Description = "Новые прорыва"
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
