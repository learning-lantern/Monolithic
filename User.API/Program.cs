using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.API.Data;
using User.API.Data.Models;
using User.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserContext>(
    optionsActions => optionsActions.UseSqlServer(connectionString: builder.Configuration.GetConnectionString(name: "UsersDB")));

builder.Services.AddIdentity<UserModel, IdentityRole>(setupAction =>
{
    setupAction.SignIn.RequireConfirmedEmail = true;
    setupAction.Password.RequiredLength = 8;
    setupAction.Password.RequireNonAlphanumeric = false;
    setupAction.User.RequireUniqueEmail = true;
    setupAction.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.SaveToken = true;
    configureOptions.RequireHttpsMetadata = false;
    configureOptions.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: builder.Configuration["JWT:IssuerSigningKey"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddCors(setupAction => setupAction.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
