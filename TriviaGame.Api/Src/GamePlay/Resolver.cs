using System;
using System.Collections.Generic;
using System.Linq;
using TriviaGame.Api.entities;
using TriviaGame.Entities;

namespace TriviaGame.Api;

public class Resolver : IResolver
{
    public QuestionState Resolve(Question q, Game g)
    {
        if (q ==null)
        {
            throw new ArgumentNullException(nameof(q), "Resolver requires a question to resolve");
        }

        if (g == null)
        {
            throw new ArgumentNullException(nameof(g), "Resolver requires a game to resolve its question");
        }

        // iterate the votes, decide what is the correct answer, if the state changes following that, give awarded points
        // the question was previously resolved and points were awarded, no need to do anything
        if (q.State is QuestionState.Unresolved or QuestionState.Resolved || q.Votes.Count < 6)
        {
            // the question was previously resolved/unresolved and points were awarded, no need to do anything
            return q.State;
        }

        // if the question wasn't resolved with 11 users it will be marked unresolved.
        // meaning, from the 12th user this applies
        if (q.Votes.Count > 11)
        {
            q.State = QuestionState.Unresolved;
            return q.State;
        }

        IEnumerable<IGrouping<int, int>> groups = q.Votes.ToGroupedByAnsId;
        List<AnsVotes> groupedVotesByAnsId = new();
        foreach (var grp in groups)
        {
            var ansVotes = new AnsVotes(grp.Key, grp.Count());
            groupedVotesByAnsId.Add(ansVotes);
        }

        var sumOfAllVotes = groupedVotesByAnsId.Sum(kc => kc.NumVotes);
        groupedVotesByAnsId.Sort();
        var maxVote = groupedVotesByAnsId[0];
        if ((decimal)maxVote.NumVotes / sumOfAllVotes > g.MajorityPercentage)
        {
            var correctAnswer = maxVote.AnsId;

            // winners are those who voted for the correct answer
            HashSet<string> winnerUsernames = q.Votes.GetPlayerNames(correctAnswer);
            g.Players.AwardPoints(winnerUsernames, g.PointsPerQuestion);

            // resolve the question
            q.State = QuestionState.Resolved;
            return q.State;
        }

        return QuestionState.Pending;
    }

    private struct AnsVotes : IComparable<AnsVotes>
    {
        public AnsVotes(int ansId, int numVotes)
        {
            AnsId = ansId;
            NumVotes = numVotes;
        }

        public int AnsId;
        public int NumVotes;

        public int CompareTo(AnsVotes other)
        {
            return -1 * (NumVotes - other.NumVotes);
        }
    }
}