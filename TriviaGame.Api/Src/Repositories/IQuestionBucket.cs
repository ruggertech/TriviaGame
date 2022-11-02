using System.Collections.Generic;
using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public interface IQuestionBucket
{
    Question GetQuestion(int Id);
    List<Question> GetQuestions(List<int> Id);
}