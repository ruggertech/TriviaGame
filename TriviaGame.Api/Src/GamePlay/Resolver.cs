using System.Linq;
using TriviaGame.Api.entities;

namespace TriviaGame.Api;

public class Resolver : IResolver
{
    public QuestionState Resolve(Question q, Game g)
    {
        // a q was answered, if it is the right one, immediately go over all the players who chose
        // this answer and give them points

        // iterate the votes, decide what is the correct answer, if the state changes following that, give awarded points
        // the question was previously resolved and points were awarded, no need to do anything
        if (q.State is QuestionState.Unresolved or QuestionState.Resolved || q.Votes.Count < 6)
        {
            // the question was previously resolved/unresolved and points were awarded, no need to do anything
            return q.State;
        }

        // state is pending
        if (q.Votes.Count > 10)
        {
            q.State = QuestionState.Unresolved;
            return q.State;
        }

        //var g = q.Votes.Values.GroupBy( i => i );
        var occurrences =
            from vote in q.Votes.Values
            group vote by vote
            into grp
            orderby grp descending
            select new
            {
                grp.Key, Count = grp.Count()
            };

        var sumOfAllVotes = occurrences.Sum(kc => kc.Count);
        var maxVote = occurrences.FirstOrDefault();
        if (maxVote.Count / sumOfAllVotes > 0.75)
        {
            var correctAnswer = maxVote.Key;

            // winners are those who voted for the correct answer
            var winnerUsernames = q.Votes
                .Where(kc => kc.Value == correctAnswer)
                .Select(kc => kc.Key)
                .ToHashSet();
            
            
            g.Players.AwardPoints(winnerUsernames, g.PointsPerQuestion);
            
            // resolve the question
            q.State = QuestionState.Resolved;
            return q.State;
        }

        return QuestionState.Pending;
    }
}