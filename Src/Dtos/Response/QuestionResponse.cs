using System.Collections.Generic;

namespace TriviaGame.entities.response
{
    public class QuestionResponse
    {
        public QuestionResponse(string questionText, List<Answer> possibleAnswers)
        {
            QuestionText = questionText;
            PossibleAnswers = possibleAnswers;
        }

        public string QuestionText { get; set; }
        public List<Answer> PossibleAnswers { get; set; }
    }
}