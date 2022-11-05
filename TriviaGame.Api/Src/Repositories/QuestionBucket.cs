using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;
using TriviaGame.Api.entities;
using TriviaGame.Api.Utils;
using TriviaGame.Entities;


namespace TriviaGame.Api.Repositories;

public sealed class QuestionBucket : IQuestionBucket
{
    private readonly List<Question> m_questions = new();
    // TODO: move numOfQuestionsToFetch to configuration
    private const int NUM_OF_QUESTIONS_TO_FETCH = 30;

    public QuestionBucket()
    {
        //var url = @"https://opentdb.com/api.php?amount=@{numOfQuestionsToFetch}&type=multiple";
        // TODO: move to configuration
        const string baseUrl = "https://opentdb.com/api.php";
        var param = new Dictionary<string, string>
        {
            { "amount", NUM_OF_QUESTIONS_TO_FETCH.ToString() },
            { "type", "multiple" }
        };
        var url = new Uri(QueryHelpers.AddQueryString(baseUrl, param)).ToString();
        var questionsFromTriviaDb = HttpUtils.GetAsync(url).Result;
        for (var i = 0; i < questionsFromTriviaDb.results.Count; i++)
        {
            var webQuestion = questionsFromTriviaDb.results[i];
            List<string> answersText = new()
            {
                // add possible answers
                webQuestion.correct_answer
            };
            answersText.AddRange(webQuestion.incorrect_answers);
            var newQuestion = new Question(i + 1, text: webQuestion.question,
                answers: answersText
            );

            m_questions.Add(newQuestion);
        }
    }

    public Question GetQuestion(int id)
    {
        // a user will not get the same question again if he has already answered it
        return m_questions.Find(q => q.Id == id);
    }

    public List<Question> GetQuestions(List<int> listOfIds)
    {
        return listOfIds.Select(GetQuestion).ToList();
    }

    public List<Question> GetQuestions(int numOfQuestions)
    {
        if (numOfQuestions > NUM_OF_QUESTIONS_TO_FETCH)
            return m_questions.GetRange(0, NUM_OF_QUESTIONS_TO_FETCH - 1);

        return m_questions.GetRange(0, numOfQuestions - 1);
    }
}