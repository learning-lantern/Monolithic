﻿using API.Admin.Repositories;
using API.Auth.Repositories;
using API.Calendar.Repositories;
using API.Classroom.Repositories;
using API.Database;
using API.ToDo.Repositories;
using API.University.Repositories;
using API.User.Models;
using API.User.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add database.
builder.Services.AddDbContext<LearningLanternContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add identity core framework roles.
builder.Services.AddIdentity<UserModel, IdentityRole>(setupAction =>
{
    setupAction.SignIn.RequireConfirmedAccount = true;
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

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services.
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IToDoRepository, ToDoRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
builder.Services.AddTransient<IClassroomRepository, ClassroomRepository>();
builder.Services.AddTransient<ICalendarRepository, CalendarRepository>();

// Add cors for Angular.
builder.Services.AddCors(setupAction => setupAction.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); // Blocking call listens to HTTP requests.