using CURD_APP.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
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

//using Microsoft.Data.SqlClient;
//using System;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// For production scenarios, consider keeping Swagger configurations behind the environment check
//// if (app.Environment.IsDevelopment())
//// {
//app.UseSwagger();
//app.UseSwaggerUI();
//// }

//app.UseHttpsRedirection();

//string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

//try
//{
//// Table would be created ahead of time in production
//using var conn = new SqlConnection(connectionString);
//conn.Open();

//var command = new SqlCommand(
//    "CREATE TABLE Persons (ID int NOT NULL PRIMARY KEY IDENTITY, FirstName varchar(255), LastName varchar(255));",
//    conn);
//using SqlDataReader reader = command.ExecuteReader();
//}
//catch (Exception e)
//{
//// Table may already exist
//Console.WriteLine(e.Message);
//}

//app.MapGet("/Person", () => {
//var rows = new List<string>();

//using var conn = new SqlConnection(connectionString);
//conn.Open();

//var command = new SqlCommand("SELECT * FROM Persons", conn);
//using SqlDataReader reader = command.ExecuteReader();

//if (reader.HasRows)
//{
//while (reader.Read())
//{
//rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetString(2)}");
//}
//}

//return rows;
//})
//.WithName("GetPersons")
//.WithOpenApi();

//app.MapPost("/Person", (Person person) => {
//using var conn = new SqlConnection(connectionString);
//conn.Open();

//var command = new SqlCommand(
//    $"INSERT INTO Persons (firstName, lastName) VALUES ('{person.FirstName}', '{person.LastName}')",
//    conn);

//using SqlDataReader reader = command.ExecuteReader();
//})
//.WithName("CreatePerson")
//.WithOpenApi();

//app.Run();
//public class Person
//{
//    public required string FirstName { get; set; }
//    public required string LastName { get; set; }
//}
