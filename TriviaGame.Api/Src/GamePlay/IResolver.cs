using TriviaGame.entities;

namespace TriviaGame;

public interface IResolver
{
    QuestionState Resolve(Question q, Game g);
}