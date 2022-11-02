using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.Api;

public class GameManager : IGameManager
{
    private IGameRepository m_gameRepository;
    private IQuestionBucket m_questionBucket;

    public GameManager(IGameRepository gameRepository, IQuestionBucket questionBucket)
    {
        m_gameRepository = gameRepository;
        m_questionBucket = questionBucket;
    }

    public string CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds)
    {
        var qList = m_questionBucket.GetQuestions(questionIds);
        var newGame = new Game(pointsPerQuestion, playerUserNames, qList);
        var gameId = m_gameRepository.AddGame(newGame);
        return gameId;
    }
}