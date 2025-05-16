using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Data
{
    public static class DbSeeder
    {
        /// <summary>
        /// Applies migrations and then, if each table is empty,
        /// inserts Instructors, Clients, Courses, Sessions, Payments.
        /// The ApplicationDbContext.SaveChanges override will
        /// stamp the TenantId on every new entity for you.
        /// </summary>
        public static void Seed(ApplicationDbContext context)
        {
            // 1) Ensure the schema is up-to-date
            context.Database.Migrate();

            // 2) Instructors
            if (!context.Instructors.Any())
            {
                context.Instructors.AddRange(
                    new Instructor
                    {
                        FullName = "John Doe",
                        Email = "john@example.com",
                        PhoneNumber = "123456789",
                        DateOfBirth = new DateTime(1985, 5, 20),
                        Department = "Pilates",
                        Bio = "Expert Pilates Instructor",
                        IsActive = true
                    },
                    new Instructor
                    {
                        FullName = "Jane Smith",
                        Email = "jane@example.com",
                        PhoneNumber = "987654321",
                        DateOfBirth = new DateTime(1990, 3, 15),
                        Department = "Yoga",
                        Bio = "Certified Yoga Trainer",
                        IsActive = true
                    }
                );
                context.SaveChanges();
            }

            // 3) Clients
            if (!context.Clients.Any())
            {
                context.Clients.AddRange(
                    new Client
                    {
                        FullName = "Alice Brown",
                        Email = "alice@example.com",
                        PhoneNumber = "555123456",
                        DateOfBirth = new DateTime(1992, 7, 10),
                        Address = "123 Main St",
                        ProfilePictureUrl = "",
                        IsActive = true
                    },
                    new Client
                    {
                        FullName = "Bob White",
                        Email = "bob@example.com",
                        PhoneNumber = "555987654",
                        DateOfBirth = new DateTime(1988, 2, 25),
                        Address = "456 Elm St",
                        ProfilePictureUrl = "",
                        IsActive = true
                    }
                );
                context.SaveChanges();
            }

            // pull back the just‐inserted rows
            var john = context.Instructors.First(i => i.Email == "john@example.com");
            var jane = context.Instructors.First(i => i.Email == "jane@example.com");
            var alice = context.Clients.First(c => c.Email == "alice@example.com");
            var bob = context.Clients.First(c => c.Email == "bob@example.com");

            // 4) Courses
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course
                    {
                        Title = "Pilates for Beginners",
                        Description = "Basic Pilates Training",
                        Instructor = john,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        Category = "Pilates",
                        DurationInHours = 10
                    },
                    new Course
                    {
                        Title = "Advanced Yoga",
                        Description = "Master Level Yoga",
                        Instructor = jane,
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(2),
                        Category = "Yoga",
                        DurationInHours = 20
                    }
                );
                context.SaveChanges();
            }

            // pull back courses
            var pilates = context.Courses.First(c => c.Title == "Pilates for Beginners");
            var yoga = context.Courses.First(c => c.Title == "Advanced Yoga");

            // 5) Sessions
            if (!context.Sessions.Any())
            {
                context.Sessions.AddRange(
                    new Session
                    {
                        Course = pilates,
                        Instructor = john,
                        Client = alice,
                        SessionDate = DateTime.UtcNow.AddDays(2),
                        SessionTime = new TimeSpan(10, 0, 0),
                        Location = "Studio A",
                        Topic = "Introduction to Pilates",
                        IsCompleted = false
                    },
                    new Session
                    {
                        Course = yoga,
                        Instructor = jane,
                        Client = bob,
                        SessionDate = DateTime.UtcNow.AddDays(3),
                        SessionTime = new TimeSpan(14, 0, 0),
                        Location = "Studio B",
                        Topic = "Advanced Yoga Poses",
                        IsCompleted = false
                    }
                );
                context.SaveChanges();
            }

            // 6) Payments
            if (!context.Payments.Any())
            {
                context.Payments.AddRange(
                    new Payment
                    {
                        Client = alice,
                        Amount = 100m,
                        PaymentDate = DateTime.UtcNow,
                        PaymentMethod = "Credit Card",
                        IsSuccessful = true,
                        IsRefunded = false
                    },
                    new Payment
                    {
                        Client = bob,
                        Amount = 200m,
                        PaymentDate = DateTime.UtcNow,
                        PaymentMethod = "PayPal",
                        IsSuccessful = true,
                        IsRefunded = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
