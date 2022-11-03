using TriviaGame.Api;
using TriviaGame.Api.entities;

namespace TriviaGame.UnitTests.GamePlay;

public class ResolverTests
{
    private List<string> m_playerUsernames = new()
    {
        "Calvin Malone",
        "Brynn Norman",
        "Yvonne Hyde",
        "Gary Mccall",
        "Slade O'Neill",
        "Logan Blair",
        "Hakeem Graves",
        "William Durham",
        "Kiona Mcintyre",
        "Cleo Sanford",
        "Declan Mckay",
        "Julian Chapman"
    };

    IReadOnlyList<string> m_answers = new List<string>
    {
        "Answer 1",
        "Answer 2",
        "Answer 3",
        "Answer 4"
    };

    IReadOnlyList<string> m_questions = new List<string>
    {
        "Question 2",
        "Question 3",
        "Question 4",
        "Question 5",
        "Question 6",
        "Question 7",
        "Question 8",
        "Question 9",
        "Question 10",
        "Question 11"
    };

    [Fact]
    public void ResolveAQuestion_NotEnoughVotesForRightAnswer_ReturnsPending()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(1, m_playerUsernames, qs);

        // Act
        // answer a question with users, and then resolve it
        var totalAnswers = 11;
        var percentageForCorrect = 0.3;
        var questionToAnswer = qs[0];

        // 6 votes, by different users
        questionToAnswer.Votes = new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 2 },
            { m_playerUsernames[4], 2 },
            { m_playerUsernames[5], 3 }
        };

        IResolver resolver = new Resolver();
        var actual = resolver.Resolve(qs[0], game);

        // Assert
        Assert.Equal(QuestionState.Pending, actual);
    }
    
    [Fact]
    public void ResolveAQuestion_EnoughVotesForRightAnswer_ReturnsResolved()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(1, m_playerUsernames, qs);

        // Act
        // answer a question with users, and then resolve it
        var totalAnswers = 11;
        var percentageForCorrect = 0.3;
        var questionToAnswer = qs[0];

        // 6 votes, by different users
        questionToAnswer.Votes = new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 1 },
            { m_playerUsernames[4], 1 },
            { m_playerUsernames[5], 3 }
        };

        IResolver resolver = new Resolver();
        var actual = resolver.Resolve(qs[0], game);

        // Assert
        Assert.Equal(QuestionState.Resolved, actual);
    }
}