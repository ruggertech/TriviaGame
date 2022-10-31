using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public interface IResolver
{
    QuestionState Resolve(Question q, Game g);
}