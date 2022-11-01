using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.Api;

public class GameManager
{
    private readonly IGameRepository m_gameRepository;
    private readonly IQuestionBucket m_questionBucket;

    public GameManager(IGameRepository mGameRepository, IQuestionBucket mQuestionBucket)
    {
        m_gameRepository = mGameRepository;
        m_questionBucket = mQuestionBucket;
    }
    public string CreateGame(GameCreateRequest ar)
    {
        var qList = ar.QuestionIds.Select(questionId => m_questionBucket.GetQuestion(questionId)).ToList();
        var newGame = new Game(ar.PointsPerQuestion, ar.PlayerUserNames, qList);
        var gameId = m_gameRepository.AddGame(newGame);
        return gameId;
    }
}