using System.Collections.Generic;

namespace TriviaGame.Dtos.Response;

public class OpentdbResponse
{
    public int response_code { get; set; }
    public List<TriviaQuestion> results { get; set; }
}

public class TriviaQuestion
{
    public string category { get; set; }
    public string type { get; set; }
    public string difficulty { get; set; }
    public string question { get; set; }
    public string correct_answer { get; set; }
    public List<string> incorrect_answers { get; set; }
}