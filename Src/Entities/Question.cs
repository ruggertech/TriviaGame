using System.Collections.Generic;
using PlayerId = System.Int32;

namespace TriviaGame.entities
{
    public class Question
    {
        // TODO: This should be atomic because many thread can change the state at the same time
        public QuestionState QuestionState = QuestionState.Pending;
        
        public Dictionary<PlayerId, Answer> Votes { get; set; }
        
        public List<Answer> PossibleAnswers { get; set; }
    }
}