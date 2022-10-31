using DefaultNamespace;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Dtos.Converters;

public static class GameExtension
{
    public static GameResponse ToDto(this Game g)
    {
        return new GameResponse(g.Id);
    }
}