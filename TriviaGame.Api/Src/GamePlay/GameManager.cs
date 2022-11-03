using System.Collections.Generic;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.Api;

public class GameManager : IGameManager
{
    private IGameRepository m_gameRepository;
    private IQuestionBucket m_questionBucket;
    private readonly IResolver m_resolver;

    public GameManager(IGameRepository gameRepository, IQuestionBucket questionBucket, IResolver resolver)
    {
        m_gameRepository = gameRepository;
        m_questionBucket = questionBucket;
        m_resolver = resolver;
    }

    public Game CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds)
    {
        var qList = m_questionBucket.GetQuestions(questionIds);
        var newGame = new Game(pointsPerQuestion, playerUserNames, qList);
        m_gameRepository.AddGame(newGame);
        return newGame;
    }

    public Question GetQuestion(string gameId, string username)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => q.State != QuestionState.Unresolved && !q.Votes.ContainsKey(username));
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

    public Players GetLeaderBoard(string gameId)
    {
        var game = m_gameRepository.GetGame(gameId);
        return game.Players;
    }
}