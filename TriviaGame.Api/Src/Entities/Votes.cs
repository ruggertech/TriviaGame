using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Username = System.String;
using AnswerId = System.Int32;

namespace TriviaGame.Api.entities;

public class Votes
{
    private ConcurrentDictionary<Username, AnswerId> m_votes;

    public int Count => m_votes.Count;

    public IEnumerable<IGrouping<int, int>> ToGroupedByAnsId => m_votes.Values.GroupBy(ansId => ansId);

    public bool DidUserVote(string username) => m_votes.ContainsKey(username);

    public HashSet<string> GetPlayerNames(int correctAnswer) => m_votes
        .Where(kc => kc.Value == correctAnswer)
        .Select(kc => kc.Key)
        .ToHashSet();

    public void SetVotes(ConcurrentDictionary<string, int> dict) => m_votes = dict;

    public void SetSingleVote(Username username, AnswerId answerId)
    {
        // a player could have only one vote per question, if it exists, it will be changed
        m_votes[username] = answerId;
    }
}