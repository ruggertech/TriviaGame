using System.Linq;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGame.Dtos.Converters;
using TriviaGame.entities;
using TriviaGame.entities.request;
using TriviaGame.entities.response;
using TriviaGame.Repositories;

namespace TriviaGame.Controllers;

[ApiController]
[Route("")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameRepository m_gameRepository;
    private readonly IQuestionBucket m_questionBucket;

    public GameController(ILogger<GameController> logger, IGameRepository mGameRepository, IQuestionBucket qb)
    {
        _logger = logger;
        m_gameRepository = mGameRepository;
        m_questionBucket = qb;
    }

    [HttpGet]
    [Route("/ping")]
    public string GetPong()
    {
        // use resolver to determine the state of the question
        // if it's unresolved, return a different question
        return "Pong ";
    }

    [HttpGet]
    [Route("/question")]
    public QuestionResponse GetQuestion(string Username, string GameId)
    {
        var game = m_gameRepository.GetGame(GameId);
        var question = game.Questions.Find(q => q.State != QuestionState.Unresolved);
        var resp =  new QuestionResponse(question.Text, question.PossibleAnswers);
        return resp;
    }

    [HttpPost]
    [Route("/question/answer")]
    public AnswerResponse PostAnswer(AnswerRequest ar)
    {
        // when posting a new answer it is time to determine the new state of the question
        // use resolver change it's state
        return new AnswerResponse();
    }

    [HttpGet]
    [Route("/leaderboard")]
    public LeaderboardResponse GetLeaderboard(LeaderboardRequest ar)
    {
        // when posting a new answer it is time to determine the new state of the question
        // use resolver change it's state
        return new LeaderboardResponse();
    }

    [HttpPost]
    [Route("/game")]
    public GameResponse CreateGame(GameCreateRequest ar)
    {
        // when posting a new answer it is time to determine the new state of the question
        // use resolver change it's state
        
        // pull questions by their question id
        var qList = ar.QuestionIds.Select(questionId => m_questionBucket.GetQuestion(questionId)).ToList();
        var newGame = new Game(ar.PointsPerQuestion, ar.PlayerUserNames, qList);
        var gameId = m_gameRepository.AddGame(newGame);
        return new GameResponse(gameId);
    }

    [HttpGet]
    [Route("/game")]
    public GameResponse GetGame(string Id)
    {
        var game = m_gameRepository.GetGame(Id);
        return game.ToDto();
    }
}