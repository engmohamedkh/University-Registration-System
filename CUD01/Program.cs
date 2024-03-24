using Booking.Infrastructure.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using University.Core.Interfaces;
using University.Core.IServices;
using University.Core.Services;
using University.Infrastructure.Dbcontext;
using University.Models;
namespace CUD01
{
    public class Program
    {
        public static object Configuration { get; private set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /////////////////////////////////////////
            ///
            builder.Services.AddDbContext<UniversityDbContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
            );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UniversityDbContext>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true; //Refresh Token
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidateAssure"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidateAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            builder.Services.AddCors(options =>
              options.AddPolicy("MyPolicy", corsPolicy =>
              {
                  corsPolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
              }
              
            ));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          //  app.UseStaticFiles();
           app.UseAuthentication(); //check JWT Token
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            //app.Use(async (Context, next) =>
            //{
            //    Console.WriteLine("before Calling Middleware 1");
            //    await next();
            //    Console.WriteLine("after Calling Middleware 1");

            //});
            //app.Use(async (Context, next) =>
            //{
            //    Console.WriteLine("before Calling Middleware 2");
            //    await next();
            //    Console.WriteLine("after Calling Middleware 2");

            //});
            app.MapControllers();

            app.Run();
        }
    }
}
