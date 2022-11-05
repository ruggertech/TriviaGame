using System;
using System.Collections.Generic;
using TriviaGame.Entities;

namespace TriviaGame.Api.entities
{
    public class Game
    {
        // TODO: move majorityVotePercentage to configuration
        public Game(string id, int pointsPerQuestion, List<string> playersUserNames, List<Question> questionList,
            decimal majorityVotePercentage = 0.75M)
        {
            if (playersUserNames == null)
            {
                throw new ArgumentNullException(nameof(playersUserNames), "a game requires players on initiation");
            }
            
            if (questionList == null)
            {
                throw new ArgumentNullException(nameof(questionList), "a game requires list of players");
            }

            Id = id;
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