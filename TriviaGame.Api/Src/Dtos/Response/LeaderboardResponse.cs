using System.Collections.Generic;
using TriviaGame.Dtos.Response;

namespace DefaultNamespace;

public class LeaderboardResponse
{
    public LeaderboardResponse(List<RankedPlayer> rankedPlayers)
    {
        RankedPlayers = rankedPlayers;
    }

    public List<RankedPlayer> RankedPlayers { get; set; }
}    


