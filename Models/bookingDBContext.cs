using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace booking.Models
{
    public partial class bookingDBContext : DbContext
    {
        public bookingDBContext()
        {
        }

        public bookingDBContext(DbContextOptions<bookingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookingtable> Bookingtables { get; set; } = null!;
        public virtual DbSet<Categorymeal> Categorymeals { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Mailsetting> Mailsettings { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<Orderhistory> Orderhistories { get; set; } = null!;
        public virtual DbSet<Ordertable> Ordertables { get; set; } = null!;
        public virtual DbSet<Photorestaurant> Photorestaurants { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Specialmeal> Specialmeals { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<Tabletype> Tabletypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=bookingDB;uid=root;pwd=duyhai123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Bookingtable>(entity =>
            {
                entity.ToTable("bookingtable");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.TableId, "tableID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.DateOrder).HasColumnName("dateOrder");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Prepay)
                    .HasMaxLength(1)
                    .HasColumnName("prepay")
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.TableId).HasColumnName("tableID");

                entity.Property(e => e.TimeOrder)
                    .HasColumnType("time")
                    .HasColumnName("timeOrder");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Bookingtables)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("bookingtable_ibfk_1");
            });

            modelBuilder.Entity<Categorymeal>(entity =>
            {
                entity.ToTable("categorymeal");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Detail)
                    .HasMaxLength(3000)
                    .HasColumnName("detail");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.Jobs)
                    .HasMaxLength(255)
                    .HasColumnName("jobs");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Mailsetting>(entity =>
            {
                entity.ToTable("mailsetting");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Mail)
                    .HasMaxLength(255)
                    .HasColumnName("mail");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("meal");

                entity.HasIndex(e => e.CateId, "cateID");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CateId).HasColumnName("cateID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.Intro)
                    .HasMaxLength(255)
                    .HasColumnName("intro");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("meal_ibfk_1");
            });

            modelBuilder.Entity<Orderhistory>(entity =>
            {
                entity.ToTable("orderhistory");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .HasColumnName("fullname");

                entity.Property(e => e.Payed)
                    .HasMaxLength(1)
                    .HasColumnName("payed")
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Ordertable>(entity =>
            {
                entity.ToTable("ordertable");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.MealId, "mealID");

                entity.HasIndex(e => e.OdHistoryId, "od_history_id");

                entity.HasIndex(e => e.TableId, "tableID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.MealId).HasColumnName("mealID");

                entity.Property(e => e.OdHistoryId).HasColumnName("od_history_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.TableId).HasColumnName("tableID");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Ordertables)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("ordertable_ibfk_2");

                entity.HasOne(d => d.OdHistory)
                    .WithMany(p => p.Ordertables)
                    .HasForeignKey(d => d.OdHistoryId)
                    .HasConstraintName("ordertable_ibfk_3");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Ordertables)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("ordertable_ibfk_1");
            });

            modelBuilder.Entity<Photorestaurant>(entity =>
            {
                entity.ToTable("photorestaurant");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Brief)
                    .HasMaxLength(255)
                    .HasColumnName("brief");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Detail)
                    .HasMaxLength(3000)
                    .HasColumnName("detail");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.RoleId, "roleID")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .HasColumnName("roleName");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("setting");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CloseTime)
                    .HasColumnType("time")
                    .HasColumnName("closeTime");

                entity.Property(e => e.DayWork)
                    .HasMaxLength(255)
                    .HasColumnName("dayWork");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(255)
                    .HasColumnName("facebook");

                entity.Property(e => e.Instagram)
                    .HasMaxLength(255)
                    .HasColumnName("instagram");

                entity.Property(e => e.LinkedIn)
                    .HasMaxLength(255)
                    .HasColumnName("linkedIn");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");

                entity.Property(e => e.OpenTime)
                    .HasColumnType("time")
                    .HasColumnName("openTime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(255)
                    .HasColumnName("twitter");
            });

            modelBuilder.Entity<Specialmeal>(entity =>
            {
                entity.ToTable("specialmeal");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.MealId, "mealID");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .HasColumnName("detail");

                entity.Property(e => e.Img)
                    .HasMaxLength(255)
                    .HasColumnName("img");

                entity.Property(e => e.MealId).HasColumnName("mealID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.SpecialmealIdNavigation)
                    .HasForeignKey<Specialmeal>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("specialmeal_ibfk_1");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.SpecialmealMeals)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("mealID");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.RoleId, "fk_role");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleID")
                    .HasComment("foreign key");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_role");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("table");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.TypeTableId, "typeTableID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Ordered)
                    .HasMaxLength(1)
                    .HasColumnName("ordered")
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .HasColumnName("tableName");

                entity.Property(e => e.TypeTableId).HasColumnName("typeTableID");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.HasOne(d => d.TypeTable)
                    .WithMany(p => p.Tables)
                    .HasForeignKey(d => d.TypeTableId)
                    .HasConstraintName("table_ibfk_1");
            });

            modelBuilder.Entity<Tabletype>(entity =>
            {
                entity.ToTable("tabletype");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.Seat).HasColumnName("seat");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .HasColumnName("status")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
