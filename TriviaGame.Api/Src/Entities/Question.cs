using System;
using System.Collections.Generic;
using TriviaGame.Api.entities;
using TriviaGame.Api.Utils;
using Username = System.String;
using AnswerId = System.Int32;

namespace TriviaGame.Entities;

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

    public void Vote(Username username, AnswerId answerId) => Votes.SetSingleVote(username, answerId);

    public QuestionState State
    {
        get => m_state;
        set => m_state = value;
    }

    public int Id { get; set; }

    public string Text { get; set; }

    public Votes Votes { get; set; }

    public List<Answer> PossibleAnswers { get; set; }
}