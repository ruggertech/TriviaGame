namespace TriviaGame.entities
{
    public interface IResolver
    {
        (QuestionState, Answer) CalcState(Question q);
    }
}