using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Rocket.API;
using Rocket.API.Models;
using Rocket.API.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", true);
}


var dbConnection = new MySqlConnectionStringBuilder
{
    Server = builder.Configuration["mariadb:server"],
    UserID = builder.Configuration["mariadb:user"],
    Password = builder.Configuration["mariadb:password"],
    Database = builder.Configuration["mariadb:database"],
    Port = uint.TryParse(builder.Configuration["mariadb:port"], out uint port) ? port : 3306,
};

builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IPlayerGameRepository, PlayerGameRepository>();
builder.Services.AddMySqlDataSource(dbConnection.ConnectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Welcome to the Rocket.API");

#region Player

app.MapPost("/player", async Task<Results<Created<PlayerModel>, Conflict<ProblemDetails>>> (PlayerModel player, IPlayerRepository repository) =>
{
    var createdPlayer = await repository.AddAsync(player);
    return TypedResults.Created(string.Empty, createdPlayer);
}).WithName("CreatePlayer");

app.MapGet("player/{id}", async (int id, IPlayerRepository repository) =>
{
    var player = await repository.GetAsync(id);
    return TypedResults.Ok(player);
}).WithName("GetPlayer");

app.MapGet("player/rocket-league-id/{id}", async (double id, IPlayerRepository repository) =>
{
    var player = await repository.GetByRocketIdAsync(id);
    return TypedResults.Ok(player);
}).WithName("GetPlayerByRocketLeague");

#endregion

#region Game

app.MapPost("/game", async Task<Results<Created<GameModel>, Conflict<ProblemDetails>>> (GameModel game, IGameRepository repository) =>
{
    var createdPlayer = await repository.AddAsync(game);
    return TypedResults.Created(string.Empty, createdPlayer);
}).WithName("CreateGame");

app.MapGet("game/{id}", async (int id, IGameRepository repository) =>
{
    var player = await repository.GetAsync(id);
    return TypedResults.Ok(player);
}).WithName("GetGame");

app.MapGet("game/rocket-league-id/{id}", async (Guid id, IGameRepository repository) =>
{
    var player = await repository.GetByRocketIdAsync(id);
    return TypedResults.Ok(player);
}).WithName("GetGameByRocketLeagueId");

#endregion

#region PlayerGame

app.MapPost("/player-game", async Task<Results<Created<PlayerGameModel>, Conflict<ProblemDetails>>> (PlayerGameModel playerGame, IPlayerGameRepository repository) =>
{
    var createdPlayer = await repository.AddAsync(playerGame);
    return TypedResults.Created(string.Empty, createdPlayer);
}).WithName("CreatePlayerGameStats");

#endregion

app.Run();
