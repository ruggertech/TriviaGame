using System.Collections.Generic;

namespace TriviaGame.entities.response
{
    public class QuestionResponse
    {
        public QuestionResponse(int questionId, string questionText, List<Answer> possibleAnswers)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            PossibleAnswers = possibleAnswers;
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> PossibleAnswers { get; set; }
    }
}