using System;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGame.Api.Dtos.Converters;
using TriviaGame.Api.entities.request;
using TriviaGame.Api.entities.response;

namespace TriviaGame.Api.Controllers;

[ApiController]
[Route("")]
public class GameController : ControllerBase
{
    private readonly IGameManager m_gameManager;
    private readonly ILogger<GameController> m_logger;

    public GameController(IGameManager gameManager, ILogger<GameController> logger)
    {
        m_gameManager = gameManager;
        m_logger = logger;
    }

    [HttpGet]
    [Route("/isAlive")]
    public DateTime IsAlive()
    {
        m_logger.LogInformation("App is alive {date}", DateTime.Now);
        return DateTime.Now;
    }

    [HttpPost]
    [Route("/game")]
    public GameCreatedResponse CreateGame(GameCreateRequest ar)
    {
        var roundedPercentage = Math.Round((decimal)ar.MajorityVotePercentage, 2);
        var game = m_gameManager.CreateGame(ar.PlayerUserNames, ar.PointsPerQuestion, ar.QuestionIds,
            roundedPercentage);
        
        return game.ToGameCreatedResponse();
    }

    [HttpGet]
    [Route("/question")]
    public QuestionResponse GetQuestion(string username, string gameId)
    {
        var res = m_gameManager.GetQuestion(gameId, username);
        var resp = res.ToQuestionResponse();
        return resp;
    }

    [HttpPost]
    [Route("/question/answer")]
    public AnswerResponse PostAnswer(AnswerRequest ar)
    {
        var res = m_gameManager.Answer(ar.GameId, ar.QuestionId, ar.Username, ar.AnswerId);
        return new AnswerResponse
        {
            QuestionState = res.questionState,
            PointsEarned = res.awardedPoints
        };
    }

    [HttpGet]
    [Route("/leaderboard")]
    public LeaderboardResponse GetLeaderboard(string gameId)
    {
        var res = m_gameManager.GetLeaderBoard(gameId);
        return res.ToLeaderboardResponse();
    }

    [HttpGet]
    [Route("/game")]
    public GameResponse GetGame(string gameId)
    {
        var game = m_gameManager.GetGame(gameId);
        return game.ToGameResponse();
    }
}