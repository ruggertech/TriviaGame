namespace TriviaGame.Dtos.Response;

public class RankedPlayer
{
    public RankedPlayer(string username, int rank, int awardedPoints)
    {
        Username = username;
        Rank = rank;
        AwardedPoints = awardedPoints;
    }

    public string Username { get; set; }   
    public int Rank { get; set; }   
    public int AwardedPoints { get; set; }   
}

