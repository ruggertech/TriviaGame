using TriviaGame.Api.entities;
using TriviaGame.Entities;

namespace TriviaGame.Api;

public interface IResolver
{
    QuestionState Resolve(Question q, Game g);
}