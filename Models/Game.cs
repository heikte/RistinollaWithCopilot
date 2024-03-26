using System.Reflection;

namespace RistinollaWithCopilot.Models
{

    public class Game
    {
        public string[] Board { get; set; } = new string[9];
        public string CurrentPlayer { get; set; } = "X";

        public string Winner { get; set; }

        public void MakeMove(int i)
        {
            if (string.IsNullOrEmpty(Board[i]))
            {
                Board[i] = CurrentPlayer;
                if (CheckWin(CurrentPlayer))
                {
                    Winner = CurrentPlayer;
                }
                else if (!Board.Any(s => string.IsNullOrEmpty(s)))
                {
                    Winner = "Tasapeli";
                }
                CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
            }
        }

        public int? GetWinningMove(string player)
        {
            for (int i = 0; i < 9; i++)
            {
                if (string.IsNullOrEmpty(Board[i]))
                {
                    Board[i] = player;
                    if (CheckWin(player))
                    {
                        Board[i] = null;
                        return i;
                    }
                    Board[i] = null;
                }
            }
            return null;
        }

        public bool CheckWin(string player)
        {
            int[][] winCombinations = new int[][]
            {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 }
            };

            return winCombinations.Any(combination => combination.All(i => Board[i] == player));
        }
    }
}