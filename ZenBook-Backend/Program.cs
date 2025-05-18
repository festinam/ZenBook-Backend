using Microsoft.EntityFrameworkCore;
using ZenBook_Backend.Data;
using ZenBook_Backend.Models;
using ZenBook_Backend.Middleware;
using ZenBook_Backend.Repositories;
using ZenBook_Backend.Service;
using ZenBook_Backend.Services;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace ZenBook_Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // 2. Define the security scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter `Bearer {token}`"
                });

                // 3. Require the scheme globally (so the Authorize button appears)
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            // Configure Database Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<TenantDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opts => {

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwt = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization();

            // This registers a generic repository for any entity.
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // This registers the course service for business logic related to courses.
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();


            //Qekjo o  e re dmth per Front vyn
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            var app = builder.Build();



            // Program.cs (after builder.Build()) -- authotication
            //using (var scope = app.Services.CreateScope())
            //{
            //  var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //foreach (var role in new[] { "Admin", "User" })
            //{
            //  if (!await roleMgr.RoleExistsAsync(role))
            //    await roleMgr.CreateAsync(new IdentityRole(role));
            //}
            //}




            // 1) Migrate & seed the Tenants table itself
            using (var scope = app.Services.CreateScope())
            {
                var tenantCtx = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
                tenantCtx.Database.Migrate();
                if (!tenantCtx.Tenants.Any())
                {
                    tenantCtx.Tenants.AddRange(
                      new Tenant { Id = "tenant1", Name = "Tenant One" },
                      new Tenant { Id = "tenant2", Name = "Tenant Two" }
                    );
                    tenantCtx.SaveChanges();
                }
            }

            // 2) Now loop _those_ tenants and seed each one into your shared tables
            using (var scope = app.Services.CreateScope())
            {
                var tenantCtx = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
                var currentTenant = scope.ServiceProvider.GetRequiredService<ICurrentTenantService>();
                var appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                foreach (var t in tenantCtx.Tenants)
                {
                    // tell your context “we are now tenant X”
                    currentTenant.TenantId = t.Id;

                    appDb.Database.Migrate();

                    // this will call your overriden SaveChanges() which
                    // stamps TenantId on all new IMustHaveTenant entities:
                    DbSeeder.Seed(appDb);
                }
            }

            //Edhe qetu  e ki nje ndryshim
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    // optionally:
                    c.RoutePrefix = string.Empty; // serve at root
                });

            }

            app.UseCors("AllowAll"); //qekjo e re per front
            app.UseHttpsRedirection();
            app.UseMiddleware<TenantResolver>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // CRUD Operation Example
            // RunCrudExample(app);

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