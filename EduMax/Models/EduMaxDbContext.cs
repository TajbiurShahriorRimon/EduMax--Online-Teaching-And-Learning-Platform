using EduMax.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    public class EduMaxDbContext : DbContext
    {
        public EduMaxDbContext() : base("name=EduMaxDbContext")
        {
            //Database.SetInitializer<EduMaxDbContext>(new DropCreateDatabaseIfModelChanges<EduMaxDbContext>());
            //Database.SetInitializer<EduMaxDbContext>(new DropCreateDatabaseAlways<EduMaxDbContext>());
            Database.SetInitializer<EduMaxDbContext>(new MigrateDatabaseToLatestVersion<EduMaxDbContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ReceiverNotice> ReceiverNotices { get; set; }
        public DbSet<UserFavoriteCourse> UserFavoriteCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //the DbModelBuilder class acts as a Fluent API to configure properties.
            base.OnModelCreating(modelBuilder);
        }
    }
}