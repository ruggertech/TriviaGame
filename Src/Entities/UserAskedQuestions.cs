using System.Collections.Generic;

namespace TriviaGame.entities
{
    public class UserAskedQuestions
    {
        public Dictionary<(Player, Question), bool> AskedQuestions { get; set; }
    }
}