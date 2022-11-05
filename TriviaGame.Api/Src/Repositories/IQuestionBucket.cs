using System.Collections.Generic;
using TriviaGame.Api.entities;
using TriviaGame.Entities;

namespace TriviaGame.Api.Repositories;

public interface IQuestionBucket
{
    Question GetQuestion(int id);
    List<Question> GetQuestions(List<int> id);
}