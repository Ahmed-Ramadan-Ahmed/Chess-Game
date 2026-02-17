using System.Text;

namespace Chess_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Game game = new Game();
            game.Start();
        }
    }
}
