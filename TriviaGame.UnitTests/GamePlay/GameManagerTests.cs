using Moq;
using TriviaGame.Api;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.UnitTests.GamePlay;

public class GameManagerTests
{
    private Mock<IGameRepository> m_gameRepositoryStub = new();
    private Mock<IQuestionBucket> m_questionBucketStub = new();

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
    public void AnswerQuestion_WithAlreadyCorrectAnswer_ReturnsAwardedPointsAsSpecifiedInGame()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(1, m_playerUsernames, qs);
        var gameManager = new GameManager(m_gameRepositoryStub.Object, m_questionBucketStub.Object, new Resolver());

        m_gameRepositoryStub.Setup(repo => repo.GetGame(It.IsAny<string>()))
            .Returns(game);

        // 6 votes, by different users
        qs[0].Votes = new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 1 },
            { m_playerUsernames[4], 1 },
            { m_playerUsernames[5], 3 }
        };

        // Act
        // answer a question with users, and then resolve it
        var actual = gameManager.Answer(game.Id, qs[0].Id,
            m_playerUsernames[0], 1);

        // Assert
        Assert.Equal(game.PointsPerQuestion, actual.awardedPoints);
    }

    [Fact]
    public void AnswerQuestion_WithQuestionNotResolved_ReturnsNoAwardedPoints()
    {
        // Arrange
        List<Question> qs = new List<Question>
        {
            new(1, "Question 1", m_answers),
        };

        var game = new Game(1, m_playerUsernames, qs);
        Mock<IResolver> resolverMock = new();
        resolverMock.Setup(resolver => resolver.Resolve(It.IsAny<Question>(), It.IsAny<Game>()))
            .Returns(QuestionState.Unresolved);
        var gameManager = new GameManager(m_gameRepositoryStub.Object, m_questionBucketStub.Object, resolverMock.Object);

        m_gameRepositoryStub.Setup(repo => repo.GetGame(It.IsAny<string>()))
            .Returns(game);

        // 6 votes, by different users
        qs[0].Votes = new Dictionary<string, int>
        {
            { m_playerUsernames[0], 1 },
            { m_playerUsernames[1], 1 },
            { m_playerUsernames[2], 1 },
            { m_playerUsernames[3], 1 },
            { m_playerUsernames[4], 1 },
            { m_playerUsernames[5], 3 }
        };

        // Act
        // answer a question with users, and then resolve it
        var actual = gameManager.Answer(game.Id, qs[0].Id,
            m_playerUsernames[0], 0);

        // Assert
        Assert.Equal(0, actual.awardedPoints);
    }
}