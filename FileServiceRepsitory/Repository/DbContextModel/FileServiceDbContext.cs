using System.Collections.Immutable;
using DataModel.User;
using Microsoft.EntityFrameworkCore;
namespace FileServiceRepsitory.Repository.DbContextModel;
public class FileServiceDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<LoginRecordDto> LoginRecords{get;set;}
    
    public FileServiceDbContext(DbContextOptions option) : base(option)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserDto>().HasKey(u=>u.Id);
        modelBuilder.Entity<UserDto>(user =>
        {

            user.Property(u => u.Id).HasColumnName("UserId").IsRequired();
            user.Property(u => u.Name).HasColumnName("UserName").HasColumnType("nvarchar(20)").HasMaxLength(20);
            user.Property(u => u.PassWord).HasColumnType("nvarchar(200)");
            user.HasKey(u => u.Id);
            user.ToTable("Users");
        });
        modelBuilder.Entity<LoginRecordDto>().HasKey(u=>u.LoginRecordId);
        modelBuilder.Entity<LoginRecordDto>(loginRecord=>{
            loginRecord.Property(r=>r.LoginRecordId).HasColumnName("LoginRecordId").HasColumnType("int").IsRequired();
            loginRecord.Property(r=>r.LoginTime).HasColumnName("LoginTime").HasColumnType("nvarchar(15)").HasMaxLength(15);
            loginRecord.Property(r=>r.LoginIp).HasColumnName("LoginIp").HasColumnType("nvarchar(20)");
            loginRecord.Property(r=>r.UserId).HasColumnName("UserId").HasColumnType("int");
        });
        #region 一对多
            modelBuilder.Entity<UserDto>().HasMany(u=>u.LoginRecords).WithOne().HasForeignKey(r=>r.UserId);
        #endregion
    }
}
