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
        "Julian Chapman",
        "Bear Walton",
        "Isobel Horton",
        "Inaaya Bowen",
        "Aishah Carson",
        "Ella-Louise Gregory",
        "Gavin Mcmahon"
    };

    IReadOnlyList<string> m_answers = new List<string>
    {
        "Answer 1",
        "Answer 2",
        "Answer 3",
        "Answer 4"
    };

    [Fact]
    public void ResolveAQuestion_NotEnoughVotesForRightAnswer_ReturnsPending()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(Guid.NewGuid().ToString(), 1, m_playerUsernames, qs);
        var questionToAnswer = qs[0];

        // 6 votes, by different users
        questionToAnswer.SetVotes(new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 2 },
            { m_playerUsernames[4], 2 },
            { m_playerUsernames[5], 3 }
        });

        // Act
        // answer a question with users, and then resolve it
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

        var game = new Game(Guid.NewGuid().ToString(), 1, m_playerUsernames, qs);

        // 6 votes, by different users
        var questionToAnswer = qs[0];
        questionToAnswer.SetVotes(new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 1 },
            { m_playerUsernames[4], 1 },
            { m_playerUsernames[5], 3 }
        });


        // Act
        // answer a question with users, and then resolve it
        IResolver resolver = new Resolver();
        var actual = resolver.Resolve(qs[0], game);

        // Assert
        Assert.Equal(QuestionState.Resolved, actual);
    }

    [Fact]
    public void ResolveAQuestion_PassedElevenVotesWithoutCorrectAnswer_ReturnsUnResolved()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(Guid.NewGuid().ToString(), 1, m_playerUsernames, qs);

        // Act
        var questionToAnswer = qs[0];

        // 6 votes, by different users
        questionToAnswer.SetVotes(new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 1 },
            { m_playerUsernames[4], 2 },
            { m_playerUsernames[5], 2 },
            { m_playerUsernames[6], 2 },
            { m_playerUsernames[7], 2 },
            { m_playerUsernames[8], 3 },
            { m_playerUsernames[9], 3 },
            { m_playerUsernames[10], 3 },
            { m_playerUsernames[11], 3 },
            { m_playerUsernames[12], 3 },
            { m_playerUsernames[13], 3 },
            { m_playerUsernames[14], 3 },
            { m_playerUsernames[15], 3 },
            { m_playerUsernames[16], 3 },
            { m_playerUsernames[17], 3 }
        });

        IResolver resolver = new Resolver();
        var actual = resolver.Resolve(qs[0], game);

        // Assert
        Assert.Equal(QuestionState.Unresolved, actual);
    }

    [Fact]
    public void ResolveAQuestion_ReachedElevenVotesWithACorrectAnswer_ReturnsResolved()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(Guid.NewGuid().ToString(), 1, m_playerUsernames, qs);

        // Act
        // answer a question with users, and then resolve it
        var questionToAnswer = qs[0];

        // 6 votes, by different users
        questionToAnswer.SetVotes(new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 2 },
            { m_playerUsernames[4], 1 },
            { m_playerUsernames[5], 1 },
            { m_playerUsernames[6], 1 },
            { m_playerUsernames[7], 2 },
            { m_playerUsernames[8], 1 },
            { m_playerUsernames[9], 1 },
            { m_playerUsernames[10], 1 }
        });

        IResolver resolver = new Resolver();
        var actual = resolver.Resolve(qs[0], game);

        // Assert
        Assert.Equal(QuestionState.Resolved, actual);
    }
}