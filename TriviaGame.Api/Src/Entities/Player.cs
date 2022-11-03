using System;

namespace TriviaGame.Api.entities
{
    public class Player
    {
        public Player(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
        public int AwardedPoints { get; set; }
    }
}