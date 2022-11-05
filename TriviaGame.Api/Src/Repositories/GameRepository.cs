using System;
using System.Collections.Concurrent;
using System.Linq;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public class GameRepository : IGameRepository
{
    // use a thread safe collection for a case where two clients add a game the same resource/list
    private readonly BlockingCollection<Game> m_games = new();

    public void AddGame(Game newGame)
    {
        m_games.Add(newGame);
    }

    public Game GetGame(string id)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        return m_games.First(g => g.Id == id);
    }
}