using APIs.Admin.Repositories;
using APIs.Auth.Repositories;
using APIs.Calendar.Repositories;
using APIs.Classroom.Repositories;
using APIs.Database;
using APIs.Helpers;
using APIs.Quiz.Repositories;
using APIs.ToDo.Repositories;
using APIs.University.Repositories;
using APIs.User.Models;
using APIs.User.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

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
        IssuerSigningKey = JWT.IssuerSigningKey
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
builder.Services.AddTransient<IClassroomRepository, ClassroomRepository>();
builder.Services.AddTransient<ICalendarRepository, CalendarRepository>();
builder.Services.AddTransient<IQuizRepository, QuizRepository>();
// TODO: Test the other services and integrate them with Classroom.
//builder.Services.AddTransient<IExamRepository, ExamRepository>();

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

app.Run(); // Blocking call listens to HTTP requests.
