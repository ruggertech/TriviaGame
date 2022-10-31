using System;
using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public class GameRepository : IGameRepository
{
    private readonly List<Game> Games = new();

    public string AddGame(Game newGame)
    {
        Games.Add(newGame);
        newGame.Id = Guid.NewGuid().ToString();
        return newGame.Id;
    }

    public Game GetGame(string Id)
    {
        return Games.FindLast(g => g.Id == Id);
    }
}