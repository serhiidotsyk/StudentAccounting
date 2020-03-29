using DAL.Entities;
using DAL.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configuration
{
    public static class SeedConfiguration
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "I am Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "Student",
                    Description = "I am Student"
                });
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Age = 30,
                    Email = "john.smith@gmail.com",
                    Password = "1234pass",
                    RegisteredDate = new DateTime(2018, 05, 15),
                    RoleId = (int)RoleType.Admin
                },
                new User
                {
                    Id = 2,
                    FirstName = "Sam",
                    LastName = "Glory",
                    Age = 18,
                    Email = "sam.glory@gmail.com",
                    Password = "1234pass",
                    RegisteredDate = new DateTime(2019, 09, 21),
                    RoleId = (int)RoleType.Student
                });
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "IT Step Academy",
                    StartDate = new DateTime(2020, 04, 21),
                    EndDate = new DateTime(2020, 06, 21),
                });
            modelBuilder.Entity<UserCourse>().HasData(
                new UserCourse
                {
                    StudentId = 2,
                    CourseId = 1,
                });
        }
    }
}
