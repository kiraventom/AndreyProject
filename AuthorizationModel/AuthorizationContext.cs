using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationModel
{
    public class AuthorizationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=authorization.db");

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        [Key]
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserLevel Level { get; set; }

        public enum UserLevel { User = 0, Admin = 1}
    }
}
