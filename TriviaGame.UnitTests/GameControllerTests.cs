using DefaultNamespace;
using Microsoft.Extensions.Logging;
using Moq;
using TriviaGame.Api.Controllers;
using TriviaGame.Api.entities;
using TriviaGame.Api.entities.response;
using TriviaGame.Api.Repositories;

namespace TriviaGame.UnitTests;

public class GameControllerTests
{
    private Mock<IGameRepository> m_gameRepositoryStub = new();
    private Mock<IQuestionBucket> m_questionBucketStub = new();

    IReadOnlyList<string> m_answers = new List<string>
    {
        "Answer 1",
        "Answer 2",
        "Answer 3",
        "Answer 4"
    };

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

    private Mock<ILogger<GameController>> m_loggerStub = new();

    [Fact]
    public void CreateGame_WithNonExistentGame_ReturnsNewGameId()
    {
        var result = CreateGame();

        // Assert
        Assert.IsType<string>(result.Id);
        Assert.NotEmpty(result.Id);
    }

    private GameResponse CreateGame()
    {
        //return new GameResponse("no id, should be implemented");
        return null;
        // // Arrange
        // m_gameRepositoryStub.Setup(repo => repo.AddGame(It.IsAny<Game>()))
        //     .Returns(Guid.NewGuid().ToString());
        //
        // for (int i = 1; i <= 10; i++)
        // {
        //     m_questionBucketStub.Setup(repo => repo.GetQuestion(i))
        //         .Returns(new Question(i, "what is your " + i, m_answers));
        // }
        //
        // var controller =
        //     new GameController(m_loggerStub.Object, m_gameRepositoryStub.Object, m_questionBucketStub.Object);
        //
        // // Act
        // var cr = new GameCreateRequest
        // {
        //     PointsPerQuestion = 12,
        //     PlayerUserNames = m_playerUsernames,
        //     QuestionIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
        // };
        //
        // var result = controller.CreateGame(cr);
        // return result;
    }

    [Fact]
    public void GetQuestion_WithUserOutOfTheGame_ReturnsError()
    {
        // // Arrange
        // var game = CreateGame();
        //
        // var gameWithId = new Game(55, null, null);
        // gameWithId.Id = game.Id;
        // m_gameRepositoryStub.Setup(repo => repo.GetGame(It.IsAny<string>()))
        //     .Returns(gameWithId);
        //
        // var controller =
        //     new GameController(m_loggerStub.Object, m_gameRepositoryStub.Object, m_questionBucketStub.Object);
        //
        // var actual = controller.GetQuestion("bla", game.Id);
        // Assert.Equal(new QuestionResponse(1, "dfsf", new List<Answer>()), actual);
    }

    [Fact]
    public void GetQuestion_WithUsernameFromGame_ReturnsQuestion()
    {
    }

    [Fact]
    public void Answer_WithLessThanSixUsers_ReturnsQuestionPending()
    {
    }

    [Fact]
    public void Answer_WithMoreThanElevenUsersOnPendingQuestion_ReturnsQuestionUnresolved()
    {
    }

    [Fact]
    public void Answer_WithEightUsersAnswersAreSplitFiftyPercent_ReturnsQuestionPending()
    {
    }

    [Fact]
    public void Answer_WithEightUsersWithMajorityVote_ReturnsQuestionResolvedAndCorrectAnswer()
    {
    }
}