using System;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Api.Dtos.Converters;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.request;
using TriviaGame.Api.entities.response;

namespace TriviaGame.Api.Controllers;

[ApiController]
[Route("")]
public class GameController : ControllerBase
{
    private readonly IGameManager m_gameManager;

    public GameController(IGameManager gameManager)
    {
        m_gameManager = gameManager;
    }

    [HttpGet]
    [Route("/isAlive")]
    public DateTime IsAlive()
    {
        return DateTime.Now;
    }

    [HttpPost]
    [Route("/game")]
    public GameResponse CreateGame(GameCreateRequest ar)
    {
        var gameId = m_gameManager.CreateGame(ar.PlayerUserNames, ar.PointsPerQuestion, ar.QuestionIds);
        return new GameResponse(gameId);
    }

    [HttpGet]
    [Route("/question")]
    public QuestionResponse GetQuestion(string Username, string GameId)
    {
        // (int questionId, string questionText, List<Answer> possibleAnswers) res =
        //     m_gameManager.GetQuestion(GameId, Username);
        // var resp = new QuestionResponse(res.questionId, res.questionText, res.possibleAnswers);
        // return resp;
        return null;
    }

    [HttpPost]
    [Route("/question/answer")]
    public AnswerResponse PostAnswer(AnswerRequest ar)
    {
        var game = new Game(55, null, null); // m_gameRepository.GetGame(ar.GameId);
        var question = game.Questions.Find(q => q.Id == ar.QuestionId);
        question.Vote(ar.Username, ar.AnswerId);

        // resolve question
        IResolver resolver = new Resolver();
        var questionState = resolver.Resolve(question, game);

        return new AnswerResponse
        {
            QuestionState = questionState,
            PointsEarned = game.Players.Find(p => p.Username == ar.Username).AwardedPoints
        };
    }

    [HttpGet]
    [Route("/leaderboard")]
    public LeaderboardResponse GetLeaderboard(LeaderboardRequest ar)
    {
        // when posting a new answer it is time to determine the new state of the question
        // use resolver change it's state
        return new LeaderboardResponse();
    }

    [HttpGet]
    [Route("/game")]
    public GameResponse GetGame(string Id)
    {
        var game = new Game(56, null, null);//m_gameRepository.GetGame(Id));
        return game.ToDto();
    }
}