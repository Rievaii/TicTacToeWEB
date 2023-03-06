using Microsoft.EntityFrameworkCore;
using TicTacToeWEB;

var builder = WebApplication.CreateBuilder(args);

using (var client = new Context())
{
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
