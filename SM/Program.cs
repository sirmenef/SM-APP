using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SM.Core.Services;
using SM.Extension;
using SM.Infrastructure.EF;
using SM.Infrastructure.Model;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
  opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  opts.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();

builder.Services.AddDbContext<AppDbContext>(opt =>
  opt.UseSqlServer(builder.Configuration.GetConnectionString("localDb")));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<FollowerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.ApplyMigrations();
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapIdentityApi<User>();
  
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
