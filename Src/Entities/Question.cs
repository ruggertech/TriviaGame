using System.Collections.Generic;
using TriviaGame.Utils;
using PlayerId = System.Int32;

namespace TriviaGame.entities
{
    public class Question
    {
        private volatile QuestionState m_state;

        public Question(int id, string text, IReadOnlyList<string> answers)
        {
            Id = id;
            Text = text;
            PossibleAnswers = new List<Answer>();
            for (var i = 0; i < answers.Count; i++)
            {
                PossibleAnswers.Add(new Answer(i + 1, answers[i]));
            }
            
            PossibleAnswers.Shuffle();  // Randomize order of answers
        }

        public QuestionState State
        {
            get => m_state;
            set => m_state = value;
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public Dictionary<PlayerId, Answer> Votes { get; set; }

        public List<Answer> PossibleAnswers { get; set; }
    }
}