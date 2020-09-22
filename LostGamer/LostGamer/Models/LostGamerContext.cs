using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LostGamer.Models
{
    public partial class LostGamerContext : DbContext
    {
        public LostGamerContext()
        {
        }

        public LostGamerContext(DbContextOptions<LostGamerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Guides> Guides { get; set; }
        public virtual DbSet<Platforms> Platforms { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Stars> Stars { get; set; }
        public virtual DbSet<UserComments> UserComments { get; set; }
        public virtual DbSet<UserGuidesColletion> UserGuidesColletion { get; set; }
        public virtual DbSet<UserProfiles> UserProfiles { get; set; }
        public virtual DbSet<UserReviews> UserReviews { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ADKNEVB\\SQLEXPRESS;Database=LostGamer;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DatePosted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserProfilesId).HasColumnName("UserProfilesID");

                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserProfilesId)
                    .HasConstraintName("FK__Comments__UserPr__5FB337D6");

                entity.Property(e => e.GuideId).HasColumnName("GuideID");

                entity.HasOne(d => d.Guides)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(d => d.GuideId)
                   .HasConstraintName("FK__Comments__Guides__607251E5");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CoverLogo)
                    .IsRequired()
                    .HasColumnName("Cover_Logo")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.GameTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformsId).HasColumnName("PlatformsID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.Synopsis)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Games__CategoryI__571DF1D5");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Games__CompanyID__59063A47");

                entity.HasOne(d => d.Platforms)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.PlatformsId)
                    .HasConstraintName("FK__Games__Platforms__5629CD9C");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK__Games__RatingID__5812160E");
            });

            modelBuilder.Entity<Guides>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateSubmitted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.GuideContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.GuideTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserProfilesId).HasColumnName("UserProfilesID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Guides)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__Guides__GameID__5CD6CB2B");

                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.Guides)
                    .HasForeignKey(d => d.UserProfilesId)
                    .HasConstraintName("FK__Guides__UserProf__5BE2A6F2");
            });

            modelBuilder.Entity<Platforms>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PlatformName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RatingNum)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateMade)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StarsId).HasColumnName("StarsID");

                entity.HasOne(d => d.Stars)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StarsId)
                    .HasConstraintName("FK__Reviews__StarsID__6754599E");
            });

            modelBuilder.Entity<Stars>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StarsNum).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<UserComments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.UserProfilesId).HasColumnName("UserProfilesID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__UserComme__Comme__6FE99F9F");

                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.UserComments)
                    .HasForeignKey(d => d.UserProfilesId)
                    .HasConstraintName("FK__UserComme__UserP__6EF57B66");
            });

            modelBuilder.Entity<UserGuidesColletion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.GuideId).HasColumnName("GuideID");

                entity.Property(e => e.UserProfilesId).HasColumnName("UserProfilesID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.UserGuidesColletion)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK__UserGuide__GameI__6B24EA82");

                entity.HasOne(d => d.Guide)
                    .WithMany(p => p.UserGuidesColletion)
                    .HasForeignKey(d => d.GuideId)
                    .HasConstraintName("FK__UserGuide__Guide__6C190EBB");

                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.UserGuidesColletion)
                    .HasForeignKey(d => d.UserProfilesId)
                    .HasConstraintName("FK__UserGuide__UserP__6A30C649");
            });

            modelBuilder.Entity<UserProfiles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK__UserProfi__UserT__4BAC3F29");
            });

            modelBuilder.Entity<UserReviews>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.UserProfilesId).HasColumnName("UserProfilesID");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.UserReviews)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK__UserRevie__Revie__73BA3083");

                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.UserReviews)
                    .HasForeignKey(d => d.UserProfilesId)
                    .HasConstraintName("FK__UserRevie__UserP__72C60C4A");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
