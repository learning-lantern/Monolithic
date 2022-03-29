using API.Admin.Repositories;
using API.Auth.Repositories;
using API.Calendar.Repositories;
using API.Classroom.Repositories;
using API.Database;
using API.Helpers;
using API.Quiz.Repositories;
using API.TextLesson.Repositories;
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

// Add identity core framework roles.
builder.Services.AddIdentity<UserModel, IdentityRole>(setupAction =>
{
    setupAction.SignIn.RequireConfirmedAccount = true;
    setupAction.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<LearningLanternContext>().AddDefaultTokenProviders();

// Add JWT authentication.
builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = JWT.ValidIssuer,
        ValidateAudience = true,
        ValidAudience = JWT.ValidAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            key: Encoding.UTF8.GetBytes(JWT.IssuerSigningKey))
    };
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Rethink about each service life time (Singleton, Scoped, or Transient).
// Add services.
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
builder.Services.AddTransient<IToDoRepository, ToDoRepository>();
// TODO: Finish the Classroom service.
builder.Services.AddTransient<IClassroomRepository, ClassroomRepository>();
// TODO: Test the other services and integrate them with Classroom.
builder.Services.AddTransient<ICalendarRepository, CalendarRepository>();
builder.Services.AddTransient<IQuizRepository, QuizRepository>();
builder.Services.AddTransient<ITextLessonRepository, TextLessonRepository>();

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
