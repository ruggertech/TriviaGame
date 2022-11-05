using System;
using System.Threading.Tasks;
using DefaultNamespace;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGame.Api.Dtos.Converters;
using TriviaGame.Api.entities.request;
using TriviaGame.Api.entities.response;
using TriviaGame.Dtos.Response;

namespace TriviaGame.Api.Controllers;

[ApiController]
[Route("")]
[TypeFilter(typeof(BadRequestExceptionFilter))]
[Produces("application/json")]
public class GameController : ControllerBase
{
    private readonly IGameManager m_gameManager;
    private readonly ILogger<GameController> m_logger;
    private readonly IValidator<GameCreateRequest> m_validatorGameCreateRequest;
    private readonly IValidator<AnswerRequest> m_validatorAnswerRequest;

    public GameController(IGameManager gameManager, ILogger<GameController> logger,
        IValidator<GameCreateRequest> validatorGameCreateRequest,
        IValidator<AnswerRequest> validatorAnswerRequest)
    {
        m_gameManager = gameManager;
        m_logger = logger;
        m_validatorGameCreateRequest = validatorGameCreateRequest;
        m_validatorAnswerRequest = validatorAnswerRequest;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameCreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GameCreatedResponse>> CreateGame(GameCreateRequest ar)
    {
        ValidationResult result = await m_validatorGameCreateRequest.ValidateAsync(ar);
        if (!result.IsValid)
        {
            return Problem(detail: result.ToString(), statusCode: StatusCodes.Status400BadRequest,
                instance: HttpContext.Request.Path);
        }

        var roundedPercentage = Math.Round((decimal)ar.MajorityVotePercentage, 2);
        var game = m_gameManager.CreateGame(ar.PlayerUserNames, ar.PointsPerQuestion, ar.QuestionIds,
            roundedPercentage);

        return game.ToGameCreatedResponse();
    }

    [HttpGet]
    [Route("/question")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<QuestionResponse> GetQuestion(string username, string gameId)
    {
        if (username.Length == 0)
        {
            return Problem(detail: "username is missing or empty", statusCode: StatusCodes.Status400BadRequest,
                instance: HttpContext.Request.Path);
        }
        if (gameId.Length == 0)
        {
            return Problem(detail: "gameId is missing or empty", statusCode: StatusCodes.Status400BadRequest,
                instance: HttpContext.Request.Path);
        }
        var res = m_gameManager.GetQuestion(gameId, username);
        var resp = res.ToQuestionResponse();
        return resp;
    }

    [HttpPost]
    [Route("/question/answer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameCreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnswerResponse>> PostAnswer(AnswerRequest ar)
    {
        ValidationResult result = await m_validatorAnswerRequest.ValidateAsync(ar);
        if (!result.IsValid)
        {
            return Problem(detail: result.ToString(), statusCode: StatusCodes.Status400BadRequest,
                instance: HttpContext.Request.Path);
        }

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