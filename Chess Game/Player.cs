using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    public class ScoreEventArgs : EventArgs
    {
        public int Score { get; }
        public ScoreEventArgs(int score)
        {
            Score = score;
        }
    }
    internal class Player
    {
        public readonly string Name;
        public readonly Colors Color;
        public bool IsWinner;
        public int Score { get; private set; }
        public Player(string name, Colors color)
        {
            Name = name;
            Color = color;
            Score = 0;
            IsWinner = false;
        }
        public override string ToString() => $"Name: {Name}, Color: {Color}, Score: {Score}";
        public void AddScore(object? sender, ScoreEventArgs e)
        {
            Score += e.Score;
            if (Score >= 100) IsWinner = true;
        }
        public void SubtractScore(object? sender, ScoreEventArgs e)
        {
            Score -= e.Score;
        }
    }
}
