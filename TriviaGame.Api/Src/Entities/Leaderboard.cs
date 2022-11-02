using System.Collections.Generic;

namespace TriviaGame.Api.entities
{
    public class Leaderboard
    {
        /// <summary>
        /// player ID to rank
        /// </summary>
        public SortedDictionary<int, int> PlayerRank { get; set; }
    }
}