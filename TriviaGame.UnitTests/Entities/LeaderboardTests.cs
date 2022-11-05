using FluentAssertions;
using TriviaGame.Api.entities;

namespace TriviaGame.UnitTests.Entities;

public class LeaderboardTests
{
    private readonly List<string> m_playerUsernames = new()
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
        board[0].AwardedPoints.Should().Be(100);
        board[3].AwardedPoints.Should().Be(80);
        board[6].AwardedPoints.Should().Be(60);
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
        awardedPoints.Should().Be(100);
    }
    
    [Fact]
    public void GetUsername_WithListOfPlayers_ReturnsTheirUsername()
    {
        // Arrange
        var lb = new Leaderboard(m_playerUsernames);

        // Act
        var usernames = lb.GetUsernames();

        // Assert
        var expected = new List<string>(m_playerUsernames.ToArray());
        expected.Sort();
        usernames.Should().Equal(expected);
    }
}