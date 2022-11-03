using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.response;
using TriviaGame.Dtos.Response;

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

    public static LeaderboardResponse ToLeaderboardResponse(this Leaderboard l)
    {
        var ranked = new List<RankedPlayer>();
        int i = 1;
        foreach (var player in l.PlayerRank)
        {
            ranked.Add(new RankedPlayer(player.Username, i, player.AwardedPoints));
            i++;
        }
        
        return new LeaderboardResponse(ranked);
    }
}