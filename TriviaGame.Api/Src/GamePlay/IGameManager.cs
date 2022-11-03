using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public interface IGameManager
{
    Game CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds);
    Question GetQuestion(string gameId, string username);

    (QuestionState questionState, int awardedPoints) Answer(string gameId, int questionId, string username,
        int answerId);

    Game GetGame(string gameId);
    Players GetLeaderBoard(string gameId);
}