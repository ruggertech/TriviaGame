using System.Collections.Generic;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.response;
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

    public Game CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds)
    {
        var qList = m_questionBucket.GetQuestions(questionIds);
        var newGame = new Game(pointsPerQuestion, playerUserNames, qList);
        m_gameRepository.AddGame(newGame);
        return newGame;
    }

    public (int questionId, string questionText, List<Answer> possibleAnswers) GetQuestion(string gameId,
        string username)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => q.State != QuestionState.Unresolved && !q.Votes.ContainsKey(username));
        return (question.Id, question.Text, question.PossibleAnswers);
    }

    public (QuestionState questionState, int awardedPoints) PostAnswer(string gameId, int questionId, string username, int answerId)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => q.Id == questionId);
        question.Vote(username, answerId);

        // resolve question
        IResolver resolver = new Resolver();
        var questionState = resolver.Resolve(question, game);
        var awardedPoints = game.Players.Find(p =>
        {
            return p.Username == username;
            
        }).AwardedPoints;
        return (questionState, awardedPoints);

    }

    public Game GetGame(string gameId)
    {
        return m_gameRepository.GetGame(gameId);
    }
}