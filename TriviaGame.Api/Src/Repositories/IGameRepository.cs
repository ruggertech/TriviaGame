using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public interface IGameRepository
{ 
    void AddGame(Game newGame);
    Game GetGame(string Id);
}