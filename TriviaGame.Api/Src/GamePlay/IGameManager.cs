using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public interface IGameManager
{
    string CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds);
    // (int questionId, string questionText, List<Answer> possibleAnswers) GetQuestion(string gameId, string username);
}