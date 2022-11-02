using System.Collections.Generic;

namespace TriviaGame.Api;

public interface IGameManager
{
    string CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds);
}