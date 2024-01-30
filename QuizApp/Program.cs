using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
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

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IQuiz, QuizRepository>();
builder.Services.AddScoped<IAnswer, AnswerRepository>();
builder.Services.AddScoped<IQuizScore, QuizScoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
