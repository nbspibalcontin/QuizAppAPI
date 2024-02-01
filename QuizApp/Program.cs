using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Repository.Implementation;
using QuizApp.Repository.Interfaces;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuizAppApiDbContext>(options =>
       options.UseSqlServer(builder.Configuration
      .GetConnectionString("MvcDnConnectionString")));

//Authentication Properties
builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<QuizAppApiDbContext>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Implementation of ModelMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Implementation of Interface and Repository
builder.Services.AddScoped<IQuiz, QuizRepository>();
builder.Services.AddScoped<IAnswer, AnswerRepository>();
builder.Services.AddScoped<IQuizScore, QuizScoreRepository>();
builder.Services.AddScoped<IUser, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
