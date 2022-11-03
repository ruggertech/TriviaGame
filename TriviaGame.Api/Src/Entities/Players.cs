using System.Collections.Generic;
using System.Linq;

namespace TriviaGame.Api.entities
{
    public class Players
    {
        public Players(List<string> usernames)
        {
            LeaderBoard = new SortedSet<Player>(new PlayerComparer());
            foreach (var username in usernames)
            {
                LeaderBoard.Add(new Player(username));
            }
        }
        
        private SortedSet<Player> LeaderBoard { get; set; }

        public void AwardPoints(HashSet<string> winningPlayer, int pointsPerQuestion)
        {
            List<Player> awardedPlayers = new();
            foreach (var player in LeaderBoard)
            {
                if (winningPlayer.Contains(player.Username))
                {
                    awardedPlayers.Add(player);
                }
            }

            foreach (var awardedPlayer in awardedPlayers)
            {
                LeaderBoard.Remove(awardedPlayer);
                awardedPlayer.AwardedPoints += pointsPerQuestion;
                LeaderBoard.Add(awardedPlayer);
            }
        }

        public List<string> GetUsernames()
        {
            return LeaderBoard.Select(p => p.Username).ToList();
        }

        public List<Player> GetLeaderboard()
        {
            return LeaderBoard.ToList();
        }

        public int GetAwardedPoints(string username)
        {
            return LeaderBoard.First(p => p.Username == username).AwardedPoints;
        }
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