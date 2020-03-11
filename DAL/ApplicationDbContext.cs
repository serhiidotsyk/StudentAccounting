﻿using DAL.Configuration;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure CourseId and UserId as the composite key
            modelBuilder.Entity<UserCourse>()
                .HasKey(uc => new { uc.CourseId, uc.UserId});

            // seeding data
            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
    }
}