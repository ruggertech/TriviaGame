using System.Collections.Generic;

namespace TriviaGame.Api.entities
{
    public class Game
    {
        public Game(int pointsPerQuestion, List<string> playersUserNames, List<Question> questionList,
            decimal majorityVotePercentage = 0.75M)
        {
            PointsPerQuestion = pointsPerQuestion;
            Players = new Leaderboard(playersUserNames);
            Questions = questionList;
            MajorityPercentage = majorityVotePercentage;
        }

        public string Id { get; set; }
        public int PointsPerQuestion { get; set; }
        public Leaderboard Players { get; set; }
        public List<Question> Questions { set; get; }
        public decimal MajorityPercentage { get; set; }
    }
}