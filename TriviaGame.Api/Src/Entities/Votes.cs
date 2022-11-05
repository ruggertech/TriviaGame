using System.Collections.Generic;
using System.Linq;
using Username = System.String;
using AnswerId = System.Int32;

namespace TriviaGame.Api.entities;

public class Votes
{
    private Dictionary<Username, AnswerId> m_Votes;

    public int Count => m_Votes.Count;
    public IEnumerable<IGrouping<int, int>> ToGroupedByAnsId => m_Votes.Values.GroupBy(ansId => ansId);
    public bool DidUserVote(string username) => m_Votes.ContainsKey(username);

    public HashSet<string> GetPlayerNames(int correctAnswer) => m_Votes
        .Where(kc => kc.Value == correctAnswer)
        .Select(kc => kc.Key)
        .ToHashSet();

    public void SetVotes(Dictionary<string, int> dict) => m_Votes = dict;

    public void SetSingleVote(Username username, AnswerId answerId)
    {
        // a player could have only one vote per question, if it exists, it will be changed
        m_Votes[username] = answerId;
    }
}