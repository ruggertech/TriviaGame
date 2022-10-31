using TriviaGame.Api.entities;

namespace TriviaGame.Api.Repositories;

public interface IQuestionBucket
{
    Question GetQuestion(int Id);
}