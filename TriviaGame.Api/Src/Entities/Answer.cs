namespace TriviaGame.Api.entities
{
    public class Answer
    {
        public Answer(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}