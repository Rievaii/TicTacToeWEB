using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.Entity;
using TicTacToeWEB.Handlers;
using TicTacToeWEB.Models.Data;

var builder = WebApplication.CreateBuilder(args);

using (var client = new Context())
{
    var Game1 = new Session()
    {
        GameOver = true,
        Player1Id = 123,
        Player2Id = 321,

        Field = new Field 
        {
            Tile0 = 'O', Tile1 = 'X', Tile2 = 'O',
            Tile3 = 'X', Tile4 = 'O', Tile5 = 'X',
            Tile6 = 'X', Tile7 = 'O', Tile8 = 'O',
            TotalMoves = 8
        },
    };
    client.Session.Add(Game1);

    client.SaveChanges();
}
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options => options.UseSqlite(@"Data Source=ProductsDB.db;Cache=Shared"));
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
