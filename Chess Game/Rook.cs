using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Rook : BaseChassPiece
    {
        public Rook(Colors color) : base(color, Types.Rook)
        {
            Movements.Add(Movement.InfLeft());
            Movements.Add(Movement.InfRight());
            Movements.Add(Movement.InfUp());
            Movements.Add(Movement.InfDown());
        }
    }
}
