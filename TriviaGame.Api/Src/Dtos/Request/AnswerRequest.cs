namespace TriviaGame.Api.entities.request
{
    public class AnswerRequest
    {
        public string Username { get; set; }
        public string GameId { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }
}