using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Pawn : BaseChassPiece
    {
        public Pawn(Colors color) : base(color, Types.Pawn)
        {
            if (color == Colors.White) Movements.Add(Movement.Down());
            else if (color == Colors.Black) Movements.Add(Movement.Up());
        }
        
        public event EventHandler<ScoreEventArgs> Promotion;
        public virtual void OnPromotion(BaseChassPiece NewPiece)
        {
            ScoreEventArgs scoreEventArgs = new ScoreEventArgs((int)NewPiece.Weight);
            Promotion.Invoke(this,scoreEventArgs);
        }
    }
}
