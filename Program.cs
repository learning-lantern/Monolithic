using System.Text;
using API.Authentication.Models;
using API.Authentication.Repositories;
using API.Database;
using API.ToDo.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add aun-eg database.
builder.Services.AddDbContext<LearningLanternContext>(
    optionsActions => optionsActions.UseSqlServer(
        connectionString: builder.Configuration.GetConnectionString(name: "aun-eg-local")));

// Add identity core framework roles.
builder.Services.AddIdentity<UserModel, IdentityRole>(setupAction =>
{
    setupAction.SignIn.RequireConfirmedEmail = true;
    setupAction.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<LearningLanternContext>().AddDefaultTokenProviders();

// Add JWT authentication.
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
        IssuerSigningKey = new SymmetricSecurityKey(
            key: Encoding.UTF8.GetBytes(
                s: builder.Configuration["JWT:IssuerSigningKey"]))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add services.
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IToDoRepository, ToDoRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add cors for Angular.
builder.Services.AddCors(setupAction => setupAction.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
