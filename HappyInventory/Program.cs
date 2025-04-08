using HappyInventory.API.Data;
using HappyInventory.API.Data.Repositories;
using HappyInventory.API.Helper.Mapping;
using HappyInventory.API.Middlewares;
using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.IRepositories;
using HappyInventory.API.Services.Immplemnt;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("con")));
            builder.Services.AddAutoMapper(typeof(ItemProfile).Assembly);
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
            })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<IWarehouseRepositry, WarehouseRepositry>();
            builder.Services.AddScoped<IItemRepositry, ItemRepositry>();
            builder.Services.AddScoped<IWarehouseService, WarehouseService>();
            builder.Services.AddScoped<IItemSservice, ItemSservice>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExeptionsMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
