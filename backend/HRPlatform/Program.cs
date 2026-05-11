
using HRPlatform.Core.RepositoryInterfaces;
using HRPlatform.Core.Services;
using HRPlatform.Core.Services.Interfaces;
using HRPlatform.Infrastructure;
using HRPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PlatformDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped<ISkillService, SkillService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}
