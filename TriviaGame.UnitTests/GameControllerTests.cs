using Castle.Core.Logging;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TriviaGame.Api.Controllers;
using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.UnitTests;

public class GameControllerTests
{
    [Fact]
    public void CreateGame_WithNonExistentGame_ReturnsNewGameId()
    {
        // Arrange
        var gameRepositoryStub = new Mock<IGameRepository>();
        gameRepositoryStub.Setup(repo => repo.AddGame(It.IsAny<Game>()))
            .Returns(Guid.NewGuid().ToString());

        var questionBucketStub = new Mock<IQuestionBucket>();
        for (int i = 1; i <= 10; i++)
        {
            IReadOnlyList<string> answers = new List<string>
            {
                "Answer 1",
                "Answer 2",
                "Answer 3",
                "Answer 4"
            };

            questionBucketStub.Setup(repo => repo.GetQuestion(i))
                .Returns(new Question(i, "what is your " + i, answers));
        }

        var loggerStub = new Mock<ILogger<GameController>>();
        var controller = new GameController(loggerStub.Object, gameRepositoryStub.Object, questionBucketStub.Object);

        // Act
        var cr = new GameCreateRequest
        {
            PointsPerQuestion = 12,
            PlayerUserNames = new List<string>
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
            },
            QuestionIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
        };

        var result = controller.CreateGame(cr);

        // Assert
        Assert.IsType<string>(result.Id);
        Assert.NotEmpty(result.Id);
    }
}