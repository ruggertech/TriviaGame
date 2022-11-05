using System.Collections.Concurrent;
using System.Linq;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public class GameRepository : IGameRepository
{
    // use a thread safe collection for a case where two clients add a game the same resource/list
    private readonly BlockingCollection<Game> Games = new();

    public void AddGame(Game newGame)
    {
        Games.Add(newGame);
    }

    public Game GetGame(string Id)
    {
        return Games.First(g => g.Id == Id);
    }
}