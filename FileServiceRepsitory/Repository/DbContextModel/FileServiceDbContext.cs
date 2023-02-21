using System.Net;
using DataModel.File;
using DataModel.User;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository.DbContextModel;

public class FileServiceDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<LoginRecordDto> LoginRecords { get; set; }

    public DbSet<FileEntity> Files { get; set; }

    public FileServiceDbContext(DbContextOptions option) : base(option) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        /// Table:Users
        modelBuilder.Entity<UserDto>(user =>
        {
            user.Property(u => u.Id).HasColumnName("UserId");
            user.Property(u => u.CreateTime).HasConversion(clr => clr.ToString("yyyy/MM/dd HH:mm:ss"), database => Convert.ToDateTime(database));
            user.Property(u => u.UpdateTime).HasConversion(clr => clr.ToString("yyyy/MM/dd HH:mm:ss"), database => Convert.ToDateTime(database));
        });
        // Table:LoginRecords
        modelBuilder.Entity<LoginRecordDto>().HasKey(u => u.Id);
        modelBuilder.Entity<LoginRecordDto>(loginRecord =>
        {
            loginRecord
                .Property(r => r.Id)
                .HasColumnName("LoginRecordId");
            loginRecord
                .Property(r => r.LoginTime)
                .HasColumnName("LoginTime").HasConversion(dateTime => dateTime.ToString("yyyy/MM/dd HH:mm:ss"), database => Convert.ToDateTime(database))
                .HasColumnType("varchar(50)");
            loginRecord
                .Property(r => r.LoginIp)
                .HasColumnName("LoginIp").HasConversion(clr => clr.ToString(), database => IPAddress.Parse(database))
                .HasColumnType("varchar(20)");
        });
        // Table:Files
        modelBuilder.Entity<FileEntity>(file =>
        {
            file.Property(f => f.Id).HasColumnName("FileId");
            file.Property(f => f.CreateTime).HasConversion(clr => clr.ToString("yyyy/MM/dd HH:mm:ss"), database => Convert.ToDateTime(database));
        });
    }
}
