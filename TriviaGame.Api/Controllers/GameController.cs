﻿using System;
using System.Collections.Generic;
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
    public GameCreatedResponse CreateGame(GameCreateRequest ar)
    {
        var game = m_gameManager.CreateGame(ar.PlayerUserNames, ar.PointsPerQuestion, ar.QuestionIds);
        return game.ToGameCreatedDto();
    }

    [HttpGet]
    [Route("/question")]
    public QuestionResponse GetQuestion(string Username, string GameId)
    {
        var res = m_gameManager.GetQuestion(GameId, Username);
        var resp = new QuestionResponse(res.questionId, res.questionText, res.possibleAnswers);
        return resp;
    }

    [HttpPost]
    [Route("/question/answer")]
    public AnswerResponse PostAnswer(AnswerRequest ar)
    {
        var res = m_gameManager.PostAnswer(ar.GameId, ar.QuestionId, ar.Username, ar.AnswerId);
        return new AnswerResponse
        {
            QuestionState = res.questionState,
            PointsEarned = res.awardedPoints
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
    public GameResponse GetGame(string gameId)
    {
        var game = m_gameManager.GetGame(gameId);
        return game.ToGameDto();
    }
}