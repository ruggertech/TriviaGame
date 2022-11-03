using TriviaGame.Api.entities;

namespace TriviaGame.UnitTests.Entities;

public class LeaderboardTests
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
    
    [Fact]
    public void AwardPoints_WithListOfWinners_ReturnsLeaderboardWinnerAtFront()
    {
        // Arrange
        var lb = new Leaderboard(m_playerUsernames);
        var winners = new HashSet<string>
        {
            m_playerUsernames[0],
            m_playerUsernames[1],
            m_playerUsernames[2],
        };
        var secondaries = new HashSet<string>
        {
            m_playerUsernames[3],
            m_playerUsernames[4],
            m_playerUsernames[5],
        };
        var thirds = new HashSet<string>
        {
            m_playerUsernames[6],
            m_playerUsernames[7],
            m_playerUsernames[8],
        };

        // Act
        lb.AwardPoints(winners, 100);
        lb.AwardPoints(secondaries, 80);
        lb.AwardPoints(thirds, 60);

        var board = lb.ToList();
        
        // Assert
        Assert.Equal(100, board[0].AwardedPoints );
        Assert.Equal(80, board[3].AwardedPoints);
        Assert.Equal(60, board[6].AwardedPoints);
    }
    
    [Fact]
    public void GetAwardedPointsOfPlayer_WithListOfWinners_ReturnsThePointsAwarded()
    {
        // Arrange
        var lb = new Leaderboard(m_playerUsernames);
        var winners = new HashSet<string>
        {
            m_playerUsernames[0],
            m_playerUsernames[1],
            m_playerUsernames[2],
        };

        // Act
        lb.AwardPoints(winners, 100);
        var awardedPoints = lb.GetAwardedPoints(m_playerUsernames[0]);

        // Assert
        Assert.Equal(100, awardedPoints);
    }
}