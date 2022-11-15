using Microsoft.EntityFrameworkCore;
using NotesAPI___with_Repository_Pattern_and_Dtos.DBControl;
using System.Text.Json.Serialization;
using AutoMapper;
using NotesAPI___with_Repository_Pattern_and_Dtos.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnectionKey")));


// adding automapper
builder.Services.AddAutoMapper(typeof(Program));

// notes repo
builder.Services.AddScoped<INotesRepository, NotesRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
