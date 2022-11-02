using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.response;

namespace TriviaGame.Api.Dtos.Converters;

public static class GameExtension
{
    public static GameResponse ToGameResponse(this Game g)
    {
        return new GameResponse(g.Id, g.PointsPerQuestion, g.Players.Select(p => p.Username).ToList(), 
            g.Questions.Select(q => q.Text).ToList());
    }
    
    public static GameCreatedResponse ToGameCreatedResponse(this Game g)
    {
        return new GameCreatedResponse(g.Id);
    }

    public static QuestionResponse ToQuestionResponse(this Question q)
    {
        return new QuestionResponse(q.Id, q.Text, q.PossibleAnswers);
    }
}