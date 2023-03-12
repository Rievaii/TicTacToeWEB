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
            Tile0 = 'o', Tile1 = 'x', Tile2 = 'o',
            Tile3 = 'x', Tile4 = 'o', Tile5 = 'x',
            Tile6 = 'x', Tile7 = 'o', Tile8 = 'o',
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
