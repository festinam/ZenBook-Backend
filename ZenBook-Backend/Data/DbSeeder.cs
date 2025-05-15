using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenBook_Backend.Models;

namespace ZenBook_Backend.Data
{
    public static class DbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {

                context.Database.Migrate();


                if (!context.Instructors.Any())
                {
                    var instructorJohn = new Instructor
                    {
                        FullName = "John Doe",
                        Email = "john@example.com",
                        PhoneNumber = "123456789",
                        DateOfBirth = new DateTime(1985, 5, 20),
                        Department = "Pilates",
                        Bio = "Expert Pilates Instructor",
                        IsActive = true
                    };

                    var instructorJane = new Instructor
                    {
                        FullName = "Jane Smith",
                        Email = "jane@example.com",
                        PhoneNumber = "987654321",
                        DateOfBirth = new DateTime(1990, 3, 15),
                        Department = "Yoga",
                        Bio = "Certified Yoga Trainer",
                        IsActive = true
                    };

                    context.Instructors.AddRange(instructorJohn, instructorJane);
                    context.SaveChanges();
                }


                if (!context.Clients.Any())
                {
                    var clientAlice = new Client
                    {
                        FullName = "Alice Brown",
                        Email = "alice@example.com",
                        PhoneNumber = "555123456",
                        DateOfBirth = new DateTime(1992, 7, 10),
                        Address = "123 Main St",
                        ProfilePictureUrl = "",
                        IsActive = true
                    };

                    var clientBob = new Client
                    {
                        FullName = "Bob White",
                        Email = "bob@example.com",
                        PhoneNumber = "555987654",
                        DateOfBirth = new DateTime(1988, 2, 25),
                        Address = "456 Elm St",
                        ProfilePictureUrl = "",
                        IsActive = true
                    };

                    context.Clients.AddRange(clientAlice, clientBob);
                    context.SaveChanges();
                }

                // Retrieve the seeded instructors and clients from the database
                var instructorJohnDb = context.Instructors.FirstOrDefault(i => i.Email == "john@example.com");
                var instructorJaneDb = context.Instructors.FirstOrDefault(i => i.Email == "jane@example.com");
                var clientAliceDb = context.Clients.FirstOrDefault(c => c.Email == "alice@example.com");
                var clientBobDb = context.Clients.FirstOrDefault(c => c.Email == "bob@example.com");

                // SEED COURSES using navigation properties
                if (!context.Courses.Any())
                {
                    var coursePilates = new Course
                    {
                        Title = "Pilates for Beginners",
                        Description = "Basic Pilates Training",
                        Instructor = instructorJohnDb, // Using navigation property
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        Category = "Pilates",
                        DurationInHours = 10
                    };

                    var courseYoga = new Course
                    {
                        Title = "Advanced Yoga",
                        Description = "Master Level Yoga",
                        Instructor = instructorJaneDb, // Using navigation property
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow.AddMonths(2),
                        Category = "Yoga",
                        DurationInHours = 20
                    };

                    context.Courses.AddRange(coursePilates, courseYoga);
                    context.SaveChanges();
                }

                // Retrieve the seeded courses
                var coursePilatesDb = context.Courses.FirstOrDefault(c => c.Title == "Pilates for Beginners");
                var courseYogaDb = context.Courses.FirstOrDefault(c => c.Title == "Advanced Yoga");

                // SEED SESSIONS using navigation properties
                if (!context.Sessions.Any())
                {
                    context.Sessions.AddRange(
                        new Session
                        {
                            Course = coursePilatesDb,    // Using navigation property
                            Instructor = instructorJohnDb,
                            Client = clientAliceDb,
                            SessionDate = DateTime.UtcNow.AddDays(2),
                            SessionTime = new TimeSpan(10, 0, 0),
                            Location = "Studio A",
                            Topic = "Introduction to Pilates",
                            IsCompleted = false
                        },
                        new Session
                        {
                            Course = courseYogaDb,       // Using navigation property
                            Instructor = instructorJaneDb,
                            Client = clientBobDb,
                            SessionDate = DateTime.UtcNow.AddDays(3),
                            SessionTime = new TimeSpan(14, 0, 0),
                            Location = "Studio B",
                            Topic = "Advanced Yoga Poses",
                            IsCompleted = false
                        }
                    );
                    context.SaveChanges();
                }

                // SEED PAYMENTS using navigation properties
                if (!context.Payments.Any())
                {
                    context.Payments.AddRange(
                        new Payment
                        {
                            Client = clientAliceDb, // Using navigation property
                            Amount = 100.00m,
                            PaymentDate = DateTime.UtcNow,
                            PaymentMethod = "Credit Card",
                            IsSuccessful = true,
                            IsRefunded = false
                        },
                        new Payment
                        {
                            Client = clientBobDb,   // Using navigation property
                            Amount = 200.00m,
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
}