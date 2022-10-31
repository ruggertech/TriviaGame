using System;

namespace TriviaGame.entities
{
    public class Player
    {
        public Player(Player p)
        {
            Username = p.Username;
            AwardedPoints = p.AwardedPoints;
        }

        public Player(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
        public int AwardedPoints { get; set; }
    }
}