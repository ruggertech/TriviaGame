using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public interface IGameManager
{
    Game CreateGame(List<string> playerUserNames, int pointsPerQuestion,
        List<int> questionIds, decimal MajorityVotePercentage);
    Question GetQuestion(string gameId, string username);

    (QuestionState questionState, int awardedPoints) Answer(string gameId, int questionId, string username,
        int answerId);

    Game GetGame(string gameId);
    Leaderboard GetLeaderBoard(string gameId);
}