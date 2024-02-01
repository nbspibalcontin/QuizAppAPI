using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Entity;
using QuizApp.Repository.Implementation;
using QuizApp.Repository.Interfaces;

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
    .AddEntityFrameworkStores<QuizAppApiDbContext>();

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
