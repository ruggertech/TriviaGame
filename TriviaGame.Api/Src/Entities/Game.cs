using System.Collections.Generic;
using System.Linq;

namespace TriviaGame.Api.entities
{
    public class Game
    {
        public Game(int pointsPerQuestion, List<string> playersUserNames, List<Question> questionList)
        {
            PointsPerQuestion = pointsPerQuestion;
            Players = new Players(playersUserNames);
            Questions = questionList;
        }

        public string Id { get; set; }
        public int PointsPerQuestion { get; set; }
        public Players Players { get; set; }
        public List<Question> Questions { set; get; }
    }
}