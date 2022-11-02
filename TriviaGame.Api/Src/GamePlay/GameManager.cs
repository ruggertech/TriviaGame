using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TriviaGame.Api.entities;
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

    public string CreateGame(List<string> playerUserNames, int pointsPerQuestion, List<int> questionIds)
    {
        var qList = m_questionBucket.GetQuestions(questionIds);
        var newGame = new Game(pointsPerQuestion, playerUserNames, qList);
        var gameId = m_gameRepository.AddGame(newGame);
        return gameId;
    }

    public (int questionId, string questionText, List<Answer> possibleAnswers) GetQuestion(string gameId,
        string username)
    {
        var game = m_gameRepository.GetGame(gameId);
        var question = game.Questions.Find(q => q.State != QuestionState.Unresolved && !q.Votes.ContainsKey(username));
        return (question.Id, question.Text, question.PossibleAnswers);
    }
}