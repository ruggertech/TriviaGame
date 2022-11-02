using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGame.Api.Dtos.Converters;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.request;
using TriviaGame.Api.entities.response;
using TriviaGame.Api.Repositories;

namespace TriviaGame.Api.Controllers;

[ApiController]
[Route("")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    // TODO: gameRepo and questionsBucket should not be here at all, and removed from start up dep injection
    // everything should be managed by the logic layer gameManager
    private readonly IGameRepository m_gameRepository;
    private readonly IQuestionBucket m_questionBucket;
    private readonly IGameManager m_gameManager;

    public GameController(ILogger<GameController> logger, IGameRepository mGameRepository, IQuestionBucket qb, IGameManager gameManager)
    {
        _logger = logger;
        m_gameRepository = mGameRepository;
        m_questionBucket = qb;
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
        var game = m_gameRepository.GetGame(GameId);
        // a user will not get unresolved or already asked questions
        var question = game.Questions
            .Find(q => q.State != QuestionState.Unresolved && !q.Votes.ContainsKey(Username));
        var resp =  new QuestionResponse(question.Id, question.Text, question.PossibleAnswers);
        return resp;
    }

    [HttpPost]
    [Route("/question/answer")]
    public AnswerResponse PostAnswer(AnswerRequest ar)
    {
        var game = m_gameRepository.GetGame(ar.GameId);
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
        var game = m_gameRepository.GetGame(Id);
        return game.ToDto();
    }
}