using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public interface IGameRepository
{
    string AddGame(Game newGame);
    Game GetGame(string Id);
}