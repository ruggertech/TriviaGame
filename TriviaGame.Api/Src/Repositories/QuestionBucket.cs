using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using TriviaGame.Api.entities;
using TriviaGame.Api.Utils;


namespace TriviaGame.Api.Repositories;

public sealed class QuestionBucket : IQuestionBucket
{
    private readonly List<Question> m_questions = new();
    private const int numOfQuestionsToFetch = 10;
    private static QuestionBucket m_Instance;

    private QuestionBucket()
    {
    }

    public static IQuestionBucket Instance()
    {
        m_Instance ??= new QuestionBucket();

        //var url = @"https://opentdb.com/api.php?amount=@{numOfQuestionsToFetch}&type=multiple";
        const string baseUrl = "https://opentdb.com/api.php";
        var param = new Dictionary<string, string>()
        {
            { "amount", numOfQuestionsToFetch.ToString() },
            { "type", "multiple" },
        };
        var url = new Uri(QueryHelpers.AddQueryString(baseUrl, param)).ToString();
        var QuestionsFromTriviaDb = HttpUtils.GetAsync(url).Result;
        for (var i = 0; i < QuestionsFromTriviaDb.results.Count; i++)
        {
            var webQuestion = QuestionsFromTriviaDb.results[i];
            List<string> answersText = new()
            {
                // add possible answers
                webQuestion.correct_answer
            };
            answersText.AddRange(webQuestion.incorrect_answers);
            var newQuestion = new Question(i+1, text: webQuestion.question,
                answers: answersText
            );

            m_Instance.m_questions.Add(newQuestion);
        }

        return m_Instance;
    }

    public Question GetQuestion(int Id)
    {
        // a user will not get the same question again if he has already answered it
        return m_questions.Find(q => q.Id == Id);
    }

    public List<Question> GetQuestions(int numOfQuestions)
    {
        if (numOfQuestions > numOfQuestionsToFetch)
            return m_questions.GetRange(0, numOfQuestionsToFetch - 1);

        return m_questions.GetRange(0, numOfQuestions - 1);
    }
}