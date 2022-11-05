using System;
using System.Collections.Generic;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;
using TriviaGame.Entities;

namespace TriviaGame.Api;

public class GameManager : IGameManager
{
    private IGameRepository m_gameRepository;
    private IQuestionBucket m_questionBucket;
    private readonly IResolver m_resolver;

    public GameManager(IGameRepository gameRepository, IQuestionBucket questionBucket, IResolver resolver)
    {
        m_gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository), "game manager requires a gameRepository");
        m_questionBucket = questionBucket ?? throw new ArgumentNullException(nameof(questionBucket), "game manager requires a questionBucket");
        m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver), "game manager requires a resolver");
    }

    public Game CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds,
        decimal majorityVotePercentage)
    {
        if (playerUserNames == null || playerUserNames.Count == 0)
        {
            throw new ArgumentNullException(nameof(playerUserNames), "playerUserNames list is empty");
        }

        if (pointsPerQuestion < 1)
        {
            throw new ArgumentException("pointsPerQuestion must be a positive number", nameof(pointsPerQuestion));
        }

        if (questionIds == null || questionIds.Count == 0)
        {
            throw new ArgumentNullException(nameof(questionIds), "questionIds list is empty");
        }

        if (majorityVotePercentage < 0 || majorityVotePercentage >= 1)
        {
            throw new ArgumentException("majorityVotePercentage must be number (0..1]", nameof(majorityVotePercentage));
        }

        var qList = m_questionBucket.GetQuestions(questionIds);
        var newGame = new Game(Guid.NewGuid().ToString(), pointsPerQuestion, playerUserNames, qList, majorityVotePercentage);
        m_gameRepository.AddGame(newGame);
        return newGame;
    }

    public Question GetQuestion(string gameId, string username)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => 
            q.State != QuestionState.Unresolved && !q.Votes.DidUserVote(username));
        return question;
    }

    public (QuestionState questionState, int awardedPoints) Answer(string gameId, int questionId, string username,
        int answerId)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => q.Id == questionId);
        question.Vote(username, answerId);

        // resolve question
        var questionState = m_resolver.Resolve(question, game);
        var awardedPoints = game.Players.GetAwardedPoints(username);

        return (questionState, awardedPoints);
    }

    public Game GetGame(string gameId)
    {
        return m_gameRepository.GetGame(gameId);
    }

    public Leaderboard GetLeaderBoard(string gameId)
    {
        var game = m_gameRepository.GetGame(gameId);
        return game.Players;
    }
}