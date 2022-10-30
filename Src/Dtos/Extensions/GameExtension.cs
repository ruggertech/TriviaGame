using DefaultNamespace;
using TriviaGame.entities;

namespace TriviaGame.Dtos.Converters;

public static class GameExtension
{
    public static GameResponse ToDto(this Game g)
    {
        return new GameResponse(g.Id);
    }
}