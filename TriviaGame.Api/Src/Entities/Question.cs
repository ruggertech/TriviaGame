using System.Collections.Generic;
using TriviaGame.Api.Utils;
using Username = System.String;
using AnswerId = System.Int32;

namespace TriviaGame.Api.entities
{
    public class Question
    {
        private volatile QuestionState m_state;

        public Question(int id, string text, IReadOnlyList<string> answers)
        {
            Id = id;
            Text = text;
            PossibleAnswers = new();
            Votes = new();
            for (var i = 0; i < answers.Count; i++)
            {
                PossibleAnswers.Add(new Answer(i + 1, answers[i]));
            }

            PossibleAnswers.Shuffle(); // Randomize order of answers
        }

        public void Vote(string username, int answerId)
        {
            // a player could have only one vote per question, if it exists, it will be changed
            Votes[username] = answerId;
        }

        public QuestionState State
        {
            get => m_state;
            set => m_state = value;
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public Dictionary<Username, AnswerId> Votes { get; set; }

        public List<Answer> PossibleAnswers { get; set; }
    }
}