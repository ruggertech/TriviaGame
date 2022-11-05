using TriviaGame.Api.entities;
using TriviaGame.Api.Repositories;

namespace TriviaGame.UnitTests.Repositories;

public class GameRepositoryTests
{
    [Fact]
    public void AddGamesToRepo_WithTwoGames_GamesAreAdded()
    {
        // Arrange
        IGameRepository gameRepo = new GameRepository();
        var game1 = new Game(Guid.NewGuid().ToString(), 888, new List<string> { "jack, julie, jane" },
            new List<Question>
            {
                new(1, "which is the biggest country in the world", new List<string>()
                {
                    "USA",
                    "Russia",
                    "Canada",
                    "China"
                })
            });

        var game2 = new Game(Guid.NewGuid().ToString(), 999, new List<string> { "halie, holland, hendrik" },
            new List<Question>
            {
                new(1, "what is the capital of sierra leon", new List<string>
                {
                    "Jerusalem",
                    "Brazzaville",
                    "Freetown",
                    "Victoria"
                })
            });        
        
        // Act
        gameRepo.AddGame(game1);
        gameRepo.AddGame(game2);
        var game3 = gameRepo.GetGame(game1.Id);
        var game4 = gameRepo.GetGame(game2.Id);

        // Assert
        Assert.Equal(game1, game3);        
        Assert.Equal(game2, game4);        
    }
}