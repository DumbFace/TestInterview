using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Core.StudentTeachers;
using CMS.Core.Teachers;
using Library.CMS.Core.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CMS.Data.EFCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentTeacher>().HasKey(s => new { s.StudentId, s.TeacherId });


            modelBuilder.Entity<StudentTeacher>()
                        .HasOne<Student>(s => s.Student)
                        .WithMany(s => s.StudentTeachers)
                        .HasForeignKey(s => s.StudentId);


            modelBuilder.Entity<StudentTeacher>()
                        .HasOne<Teacher>(s => s.Teacher)
                        .WithMany(s => s.StudentTeachers)
                        .HasForeignKey(s => s.TeacherId);

            // modelBuilder.Entity<Student>()
            //             .HasMany(s => s.Teachers)
            //             .WithMany(s => s.Students);

            // modelBuilder.Entity<Teacher>()
            //             .HasMany(s => s.Students)
            //             .WithMany(s => s.Teachers);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }


    public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\EXPRESSKHANG;Database=InterviewDB;Trusted_Connection=True;TrustServerCertificate=True");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}