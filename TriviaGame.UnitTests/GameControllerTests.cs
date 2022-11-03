using Microsoft.Extensions.Logging;
using Moq;
using TriviaGame.Api.Controllers;
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
}