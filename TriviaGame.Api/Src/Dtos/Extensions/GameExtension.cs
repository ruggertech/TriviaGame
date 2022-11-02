using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Dtos.Converters;

public static class GameExtension
{
    public static GameResponse ToGameDto(this Game g)
    {
        return new GameResponse(g.Id, g.PointsPerQuestion, g.Players.Select(p => p.Username).ToList(), 
            g.Questions.Select(q => q.Text).ToList());
    }
    
    public static GameCreatedResponse ToGameCreatedDto(this Game g)
    {
        return new GameCreatedResponse(g.Id);
    }
}