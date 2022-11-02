using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public interface IGameManager
{
    Game CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds);
    (int questionId, string questionText, List<Answer> possibleAnswers) GetQuestion(string gameId, string username);

    (QuestionState questionState, int awardedPoints) PostAnswer(string gameId, int questionId, string username,
        int answerId);

    Game GetGame(string gameId);
}