using System.Collections.Generic;

namespace TriviaGame.Api.entities
{
    public class Leaderboard
    {
        /// <summary>
        /// player ID to rank
        /// </summary>
        public Dictionary<int, int> PlayerRank { get; set; }
    }
}