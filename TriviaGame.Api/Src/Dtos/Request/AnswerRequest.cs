using DefaultNamespace;
using FluentValidation;

namespace TriviaGame.Api.entities.request;

public class AnswerRequest
{
    public string Username { get; set; }
    public string GameId { get; set; }
    public int AnswerId { get; set; }
    public int QuestionId { get; set; }
}

public class AnswerRequestValidator : AbstractValidator<AnswerRequest>
{
    public AnswerRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Please provide a {PropertyName} body param");
        RuleFor(x => x.GameId)
            .NotEmpty()
            .WithMessage("Please provide a {PropertyName} body param");
        RuleFor(x => x.AnswerId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Please provide a {PropertyName} body param");
        RuleFor(x => x.QuestionId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Please provide a {PropertyName} body param");
    }
}