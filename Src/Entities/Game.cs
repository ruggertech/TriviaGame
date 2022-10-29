using System.Collections.Generic;

namespace TriviaGame.entities
{
    public class Game
    {
        public Game(int pointsPerQuestion, List<Player> players, List<Question> questions)
        {
            PointsPerQuestion = pointsPerQuestion;
            Players = players;
            Questions = questions;
        }

        public int PointsPerQuestion { get; set; }
        public List<Player> Players { get; set; }
        public List<Question> Questions { set; get; }
        public Leaderboard Leaderboard { get; set; }
    }
    
    
}