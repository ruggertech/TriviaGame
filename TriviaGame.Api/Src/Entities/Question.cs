using System;
using System.Collections.Generic;
using System.Linq;
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
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers), "Question must be initialized with a list of answers");
            }

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

        private Dictionary<Username, AnswerId> Votes { get; set; }

        public List<Answer> PossibleAnswers { get; set; }
        public int CountVotes => Votes.Count;
        public  IEnumerable<IGrouping<int, int>> VotesGroupedByAnsId => Votes.Values.GroupBy(ansId => ansId);
        public bool DidUserVote(string username) => Votes.ContainsKey(username);
        public HashSet<string> GetPlayerNames(int correctAnswer) => Votes
                .Where(kc => kc.Value == correctAnswer)
                .Select(kc => kc.Key)
                .ToHashSet();

        public void SetVotes(Dictionary<string, int> dict) => Votes = dict;
    }
}