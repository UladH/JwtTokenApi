using AppConfiguration;
using AppConfiguration.Constants;
using Domain.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppDbContext
{
    public class AppDbContext : IdentityDbContext<User, UserRole, string>, IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }

        private IAppConfiguration appConfiguration;

        #region constructor

        public AppDbContext(DbContextOptions<AppDbContext> options, IAppConfiguration appConfiguration)
            : base(options)
        {
            this.appConfiguration = appConfiguration;

            Database.EnsureCreated();
        }

        #endregion

        #region protected

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var connectionString = appConfiguration.Get(ConnectionStrings.Default);

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");

            modelBuilder.Entity<RefreshToken>().HasIndex(t => t.Token).IsUnique();
        }

        #endregion
    }
}
