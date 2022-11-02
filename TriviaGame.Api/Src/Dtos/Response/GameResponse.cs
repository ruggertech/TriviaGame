using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace DefaultNamespace;

public class GameResponse
{
    public GameResponse(string id, int pointsPerQuestion, List<string> playerUsernames, List<string> questionTexts)
    {
        Id = id;
        PointsPerQuestion = pointsPerQuestion;
        PlayerUsernames = playerUsernames;
        QuestionTexts = questionTexts;
    }

    public string Id { get; set; }
    public int PointsPerQuestion { get; set; }
    public List<string> PlayerUsernames { get; set; }
    public List<string> QuestionTexts { set; get; }
}