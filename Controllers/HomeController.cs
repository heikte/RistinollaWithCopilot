using Microsoft.AspNetCore.Mvc;
using RistinollaWithCopilot.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace RistinollaWithCopilot.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private static Game _game = new Game();

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View(_game);
		}

		[HttpPost]
		public IActionResult MakeMove(int i)
		{
			_game.MakeMove(i);
			if (_game.CurrentPlayer == "O" && _game.Winner != "Tasapeli")
			{
                // Improved AI logic: make a winning move, block the player's winning move, or make a random move
                int? aiMove = _game.GetWinningMove("O") ?? _game.GetWinningMove("X");
                if (aiMove == null )
                {
                    var random = new Random();
                    do
                    {
                        aiMove = random.Next(9);
                    } while (!string.IsNullOrEmpty(_game.Board[aiMove.Value]));
                }
                _game.MakeMove(aiMove.Value);
            }
			return RedirectToAction("Index");
		}

        public IActionResult NewGame()
		{
			_game = new Game();
			return RedirectToAction("Index");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}




