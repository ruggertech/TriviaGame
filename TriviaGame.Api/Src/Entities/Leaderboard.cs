using System.Collections.Generic;
using System.Linq.Expressions;

namespace TriviaGame.Api.entities
{
    public class Leaderboard
    {
        public Leaderboard(List<string> usernames)
        {
            PlayerRank = new SortedSet<Player>(new PlayerComparer());
            foreach (var username in usernames)
            {
                PlayerRank.Add(new Player(username));
            }
        }

        public SortedSet<Player> PlayerRank { get; set; }
    }
    
    public class PlayerComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if (x.AwardedPoints == y.AwardedPoints)
            {
                return string.Compare(x.Username, y.Username);
            }

            //return x.AwardedPoints - y.AwardedPoints;
            return y.AwardedPoints - x.AwardedPoints;  // sort in descending order
        }
    }
}