using TriviaGame.entities;

namespace TriviaGame.Repositories;

public interface IGameRepository
{
    string AddGame(Game newGame);
    Game GetGame(string Id);
}