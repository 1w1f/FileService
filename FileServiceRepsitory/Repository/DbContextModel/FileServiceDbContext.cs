using DataModel.User;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository.DbContextModel;

public class FileServiceDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<LoginRecordDto> LoginRecords { get; set; }

    public FileServiceDbContext(DbContextOptions option) : base(option) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserDto>(user =>
        {
            user.Property(u => u.Id).HasColumnName("UserId");
            user.ToTable("Users");
        });
        modelBuilder.Entity<LoginRecordDto>().HasKey(u => u.Id);
        modelBuilder.Entity<LoginRecordDto>(loginRecord =>
        {
            loginRecord
                .Property(r => r.Id)
                .HasColumnName("LoginRecordId");
            loginRecord
                .Property(r => r._loginTime)
                .HasColumnName("LoginTime")
                .HasColumnType("nvarchar(50)");
            loginRecord
                .Property(r => r._loginTime)
                .HasColumnName("LoginIp")
                .HasColumnType("nvarchar(20)");
        });
    }
}
