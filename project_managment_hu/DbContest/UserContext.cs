using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_managment_hu.Model;

namespace project_managment_hu.DbContest
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> userModels { get; set; }
        public DbSet<Projects> projects { get; set; }

        public DbSet<Issuses> issuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Issuses>()
                .HasOne(i => i.Assignee)
                .WithMany(u => u.AssignedIssues)
                .HasForeignKey(i => i.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public override int SaveChanges()
        {
            var currentTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.Entity is Issuses && (e.State == EntityState.Added)))
            {
                ((Issuses)entry.Entity).CreateTime = currentTime;
                ((Issuses)entry.Entity).UpdateTime = currentTime;
            }

            return base.SaveChanges();
        }



        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}