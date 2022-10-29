using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriviaGame.entities.request;
using TriviaGame.entities.response;

namespace TriviaGame.Controllers
{
    [ApiController]
    [Route("")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/ping")]
        public string GetPong()
        {
            // use resolver to determine the state of the question
            // if it's unresolved, return a different question
            return "Pong";
        }
        
        [HttpGet]
        [Route("/question")]
        public QuestionResponse GetQuestion(QuestionRequest qr)
        {
            // use resolver to determine the state of the question
            // if it's unresolved, return a different question
            return new QuestionResponse();
        }
        
        [HttpPost]
        [Route("/question/answer")]
        public AnswerResponse PostAnswer(AnswerRequest ar)
        {
            // when posting a new answer it is time to determine the new state of the question
            // use resolver change it's state
            return new AnswerResponse();
        }
        
        [HttpGet]
        [Route("/leaderboard")]
        public LeaderboardResponse GetLeaderboard(LeaderboardRequest ar)
        {
            // when posting a new answer it is time to determine the new state of the question
            // use resolver change it's state
            return new LeaderboardResponse();
        }
        
        [HttpPost]
        [Route("/game")]
        public GameCreateResponse CreateGame(GameCreateRequest ar)
        {
            // when posting a new answer it is time to determine the new state of the question
            // use resolver change it's state
            return new GameCreateResponse();
        }
    }
}
