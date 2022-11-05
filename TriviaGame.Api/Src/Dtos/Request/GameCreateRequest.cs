using System.Collections.Generic;
using FluentValidation;

namespace DefaultNamespace;

public class GameCreateRequest
{
    public int PointsPerQuestion { get; set; }
    public List<string> PlayerUserNames { get; set; }
    public List<int> QuestionIds { set; get; }
    public double MajorityVotePercentage { set; get; }
}

public class GameCreateRequestValidator : AbstractValidator<GameCreateRequest>
{
    public GameCreateRequestValidator()
    {
        RuleFor(x => x.PointsPerQuestion)
            .GreaterThan(0)
            .WithMessage("Please provide a positive integer for PointsPerQuestion");
        RuleFor(x => x.PlayerUserNames)
            .Must(collection => collection != null && collection.Count > 0)
            .WithMessage("Please provide a list of player names");
        RuleFor(x => x.QuestionIds)
            .Must(collection => collection != null && collection.Count > 0)
            .WithMessage("Please provide a list of question ids");
        RuleForEach(x => x.QuestionIds)
            .GreaterThan(0)
            .WithMessage("QuestionIds must all be greater than zero");        
        RuleFor(x => x.MajorityVotePercentage)
            .GreaterThanOrEqualTo(0)
            .LessThan(1)
            .WithMessage("MajorityVotePercentage should be a numeric value [0..1)");
    }
}
