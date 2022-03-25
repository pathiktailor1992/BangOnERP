using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace BangOnERP.DataBase
{
    public partial class BangOnERPContext : DbContext
    {
        public BangOnERPContext()
        {
        }

        public BangOnERPContext(DbContextOptions<BangOnERPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginSuperLogin> LoginSuperLogins { get; set; }
        public virtual DbSet<MasterCity> MasterCities { get; set; }
        public virtual DbSet<MasterCountry> MasterCountries { get; set; }
        public virtual DbSet<MasterState> MasterStates { get; set; }
        public virtual DbSet<UserLoginType> UserLoginTypes { get; set; }
        public virtual DbSet<VwCityList> VwCityLists { get; set; }
        public virtual DbSet<VwLoginLoginUser> VwLoginLoginUsers { get; set; }
        public virtual DbSet<VwStateList> VwStateLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HHH3QT6\\SQLEXPRESS;Database=BangOnERP;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<LoginSuperLogin>(entity =>
            {
                entity.ToTable("LOGIN_SUPER_LOGIN");

                entity.Property(e => e.LoginSuperLoginId).HasColumnName("LOGIN_SUPER_LOGIN_ID");

                entity.Property(e => e.LoginActive)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN_ACTIVE");

                entity.Property(e => e.LoginCreatedBy).HasColumnName("LOGIN_CREATED_BY");

                entity.Property(e => e.LoginCreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGIN_CREATED_DATE");

                entity.Property(e => e.LoginPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN_PASSWORD");

                entity.Property(e => e.LoginUpdatedBy).HasColumnName("LOGIN_UPDATED_BY");

                entity.Property(e => e.LoginUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGIN_UPDATED_DATE");

                entity.Property(e => e.LoginUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN_USERNAME");

                entity.HasOne(d => d.UserLoginType)
                    .WithMany(p => p.LoginSuperLogins)
                    .HasForeignKey(d => d.UserLoginTypeId)
                    .HasConstraintName("FK_LOGIN_SUPER_LOGIN_USER_LOGIN_TYPE");
            });

            modelBuilder.Entity<MasterCity>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("MASTER_CITY");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.MasterCities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_MASTER_CITY_MASTER_STATE");
            });

            modelBuilder.Entity<MasterCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("MASTER_COUNTRY");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreaedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterState>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("MASTER_STATE");

                entity.Property(e => e.CreaedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.MasterStates)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_MASTER_STATE_MASTER_COUNTRY");
            });

            modelBuilder.Entity<UserLoginType>(entity =>
            {
                entity.ToTable("USER_LOGIN_TYPE");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date");

                entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_Date");

                entity.Property(e => e.UserLoginType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UserLoginType");
            });

            modelBuilder.Entity<VwCityList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_CityList");

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(50);
            });

            modelBuilder.Entity<VwLoginLoginUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Login_LoginUser");

                entity.Property(e => e.LoginActive)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN_ACTIVE");

                entity.Property(e => e.LoginCreatedBy).HasColumnName("LOGIN_CREATED_BY");

                entity.Property(e => e.LoginCreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGIN_CREATED_DATE");

                entity.Property(e => e.LoginSuperLoginId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGIN_SUPER_LOGIN_ID");

                entity.Property(e => e.LoginUpdatedBy).HasColumnName("LOGIN_UPDATED_BY");

                entity.Property(e => e.LoginUpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGIN_UPDATED_DATE");

                entity.Property(e => e.LoginUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("LOGIN_USERNAME");
            });

            modelBuilder.Entity<VwStateList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StateList");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
