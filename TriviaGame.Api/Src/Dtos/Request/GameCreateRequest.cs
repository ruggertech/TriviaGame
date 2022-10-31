using System.Collections.Generic;

namespace DefaultNamespace;

public class GameCreateRequest
{
    public int PointsPerQuestion { get; set; }
    public List<string> PlayerUserNames { get; set; }
    public List<int> QuestionIds { set; get; }
}