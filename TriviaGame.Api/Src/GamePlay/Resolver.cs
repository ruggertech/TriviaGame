using System;
using System.Collections.Generic;
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

        // if the question wasn't resolved with 11 users it will be marked unresolved.
        // meaning, from the 12th user this applies
        if (q.Votes.Count > 11)
        {
            q.State = QuestionState.Unresolved;
            return q.State;
        }

        var groups = q.Votes.Values.GroupBy(ansId => ansId);
        List<AnsVotes> groupedVotesByAnsId = new();
        foreach (var grp in groups)
        {
            var ansVotes = new AnsVotes(grp.Key, grp.Count());
            groupedVotesByAnsId.Add(ansVotes);
        }

        var sumOfAllVotes = groupedVotesByAnsId.Sum(kc => kc.NumVotes);
        groupedVotesByAnsId.Sort();
        var maxVote = groupedVotesByAnsId[0];
        if ((double)maxVote.NumVotes / sumOfAllVotes > 0.75)
        {
            var correctAnswer = maxVote.AnsId;

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