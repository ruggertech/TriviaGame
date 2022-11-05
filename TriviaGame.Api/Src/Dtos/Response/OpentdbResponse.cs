using System.Collections.Generic;

namespace TriviaGame.Api.Dtos.Response;

// ReSharper disable once InconsistentNaming
public class OpentdbResponse
{
    public int response_code { get; set; }
    public List<TriviaQuestion> results { get; set; }
}

// ReSharper disable once InconsistentNaming
public class TriviaQuestion
{
    public string category { get; set; }
    public string type { get; set; }
    public string difficulty { get; set; }
    public string question { get; set; }
    public string correct_answer { get; set; }
    public List<string> incorrect_answers { get; set; }
}