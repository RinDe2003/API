using Repo_API_1721030646.Helpers;
using Microsoft.EntityFrameworkCore;
using Repo_API_1721030646.Data;
using Repo_API_1721030646.Repo.Simple;
using Repo_API_1721030646.Repo.Generic_W1;
using Repo_API_1721030646.Models;
using System.Text.Json.Serialization;
using Repo_API_1721030646.Repo.Generic_W2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

//// Register Repo
builder.Services.MyConfigureRepositoryService();

// Fixing the error “A possible object cycle was detected”
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//// Register your DbContext 
var getConnectionStr = builder.Configuration.GetConnectionString("MyConnectString");
builder.Services.AddDbContext<ApiteachingContext>(option => option.UseSqlServer(getConnectionStr));

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
