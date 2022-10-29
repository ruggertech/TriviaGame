using System.Collections.Generic;

namespace TriviaGame.entities.response
{
    public class QuestionResponse
    {
        public Question Question { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> PossibleAnswers { get; set; }
    }
}