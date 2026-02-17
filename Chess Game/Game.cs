using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    enum WinnerStatus
    {
        player1,
        player2,
        draw,
        none
    }
    internal class Game
    {
        private readonly Player player1;
        private readonly Player player2;
        private readonly ChessBoard board;
        public WinnerStatus winnerStatus;
        private bool player1Turn { get; set; } = false;
        public Game()
        {
            Console.WriteLine("Welcome to Chess Game!\n");

            winnerStatus = WinnerStatus.none;
            board = new ChessBoard();

            player1 = GetPlayer(1);
            player2 = GetPlayer(2);
            board.Subscribe(player1);
            board.Subscribe(player2);
        }
        private Player GetPlayer(int playerNumber)
        {
            Console.Write($"Enter Player {playerNumber} Name: ");
            string playerName = Console.ReadLine();
            Player player = new Player(playerName, playerNumber == 1 ? Colors.White : Colors.Black);

            return player;
        }
        
        private void PrintScore()
        {
            Console.WriteLine($"Score : {player1.Name} {player1.Score} : {player2.Name} {player2.Score}\n");
        }
        public void Start()
        {
            while (winnerStatus == WinnerStatus.none)
            {
                player1Turn = !player1Turn;

                PrintScore();
                Console.WriteLine(player1Turn ? $"{player1.Name}'s Turn" : $"{player2.Name}'s Turn");
                board.printBoard();

                var player = player1Turn ? player1 : player2;

                if (!board.CanMoveAny(player.Color))
                {
                    Position position = board.GetKingPosition(player.Color);
                    
                    if(board.IsThearten(position,player.Color))
                        winnerStatus = (player1Turn ? WinnerStatus.player2 : WinnerStatus.player1);
                    
                    else winnerStatus = WinnerStatus.draw;

                    break;
                }

                bool validMove = false;
                while (!validMove)
                {
                    Console.Write($"{player.Name}: Enter square of the piece you want to move from: ");
                    string from = Console.ReadLine();

                    from = from.ToUpper();

                    if (!board.vaildFromCell(player.Color,from))
                    {
                        Console.WriteLine("Invalid move, try again.");
                        continue;
                    }

                    Console.Write($"{player.Name}: Enter square you want to move to: ");
                    string to = Console.ReadLine();

                    to = to.ToUpper();

                    if (!board.vaildToCell(player.Color,to))
                    {
                        Console.WriteLine("Invalid move, try again.");
                        continue;
                    }

                    if (!board.Move(player.Color, from, to))
                    {
                        Console.WriteLine("Invalid move, try again.");
                        continue;
                    }

                    validMove = true;
                }

                if (player.IsWinner)
                {
                    winnerStatus = player1Turn ? WinnerStatus.player1 : WinnerStatus.player2;
                    break;
                }

            }

            PrintScore();
            board.printBoard();

            Console.WriteLine(winnerStatus == WinnerStatus.draw ? "It's a draw!" :
                $"{(winnerStatus == WinnerStatus.player1 ? player1.Name : player2.Name)} wins!");
        }
    }
}
