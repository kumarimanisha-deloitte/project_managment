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

        public DbSet<labels> labels { get; set; }
        public DbSet<IssueLabel> IssueLabels { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IssueLabel>()
            .HasKey(il => new { il.IssueId, il.LabelId });

        modelBuilder.Entity<IssueLabel>()
            .HasOne(il => il.Issue)
            .WithMany(i => i.IssueLabels)
            .HasForeignKey(il => il.IssueId);

        modelBuilder.Entity<IssueLabel>()
            .HasOne(il => il.Label)
            .WithMany(l => l.IssueLabels)
            .HasForeignKey(il => il.LabelId);
    }



        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}