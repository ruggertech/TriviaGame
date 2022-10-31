using TriviaGame.entities;

namespace TriviaGame.Repositories;

public interface IQuestionBucket
{
    Question GetQuestion(int Id);
}