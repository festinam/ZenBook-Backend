using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Data;
using ZenBook_Backend.Models;

namespace ZenBook_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            // CRUD Operation Example
            RunCrudExample(app);

            app.Run();
        }

        // A simple CRUD operation for Client entity in Program.cs
        private static void RunCrudExample(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // CREATE: Add a new client
                var client = new Client
                {
                    FullName = "Festina Mjeku",
                    Email = "festina.mjeku@gmail.com",
                    PhoneNumber = "123-456-7890",
                    DateOfBirth = DateTime.Parse("2003-07-02"),
                    Address = "123 Main St",
                    ProfilePictureUrl = "https://example.com/profile.jpg",
                    IsActive = true
                };

                context.Clients.Add(client);
                context.SaveChanges();
                Console.WriteLine($"Client created with ID: {client.Id}");

                // READ: Get all clients
                var clients = context.Clients.ToList();
                Console.WriteLine("All Clients:");
                foreach (var c in clients)
                {
                    Console.WriteLine($"{c.Id}: {c.FullName}, {c.Email}");
                }

                // UPDATE: Update a client (assuming client.Id == 1)
                var clientToUpdate = context.Clients.FirstOrDefault(c => c.Id == 1);
                if (clientToUpdate != null)
                {
                    clientToUpdate.FullName = "Updated Name";
                    context.SaveChanges();
                    Console.WriteLine("Client updated!");
                }

                // DELETE: Delete a client (assuming client.Id == 1)
                var clientToDelete = context.Clients.FirstOrDefault(c => c.Id == 1);
                if (clientToDelete != null)
                {
                    context.Clients.Remove(clientToDelete);
                    context.SaveChanges();
                    Console.WriteLine("Client deleted!");
                }
            }
        }
    }
}
