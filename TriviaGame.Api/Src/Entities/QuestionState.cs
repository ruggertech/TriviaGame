using System.Text.Json.Serialization;

namespace TriviaGame.Api.entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum QuestionState
    {
        Pending = 0,
        Resolved = 1,
        Unresolved = 2
    }
}