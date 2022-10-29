using System;

namespace TriviaGame.entities
{
    public class Player
    {
        public Player(Player p)
        {
            Id = p.Id;
            AwardedPoints = p.AwardedPoints;
        }

        public int Id { get; set; }
        public int AwardedPoints { get; set; }
    }
}