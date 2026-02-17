using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class Movement
    {
        public readonly int RowChange;
        public readonly int ColumnChange;
        public readonly int MovesLimit;

        public Movement(int rowChange, int columnChange, int movesLimit)
        {
            RowChange = rowChange;
            ColumnChange = columnChange;
            MovesLimit = movesLimit;
        }

        public static Movement InfLeft()
        {
            return new Movement(0, -1, int.MaxValue);
        }
        public static Movement InfRight() 
        {
            return new Movement(0, 1, int.MaxValue);
        }
        public static Movement InfUp()
        {
            return new Movement(-1, 0, int.MaxValue);
        }
        public static Movement InfDown()
        {
            return new Movement(1, 0, int.MaxValue);
        }
        public static Movement InfUpLeft()
        {
            return new Movement(-1, -1, int.MaxValue);
        }
        public static Movement InfUpRight()
        {
            return new Movement(-1, 1, int.MaxValue);
        }
        public static Movement InfDownLeft()
        {
            return new Movement(1, -1, int.MaxValue);
        }
        public static Movement InfDownRight()
        {
            return new Movement(1, 1, int.MaxValue);
        }
        public static Movement Left()
        {
            return new Movement(0, -1, 1);
        }
        public static Movement Right()
        {
            return new Movement(0, 1, 1);
        }
        public static Movement Up()
        {
            return new Movement(-1, 0, 1);
        }
        public static Movement Down()
        {
            return new Movement(1, 0, 1);
        }
        public static Movement UpLeft()
        {
            return new Movement(-1, -1, 1);
        }
        public static Movement UpRight()
        {
            return new Movement(-1, 1, 1);
        }
        public static Movement DownLeft()
        {
            return new Movement(1, -1, 1);
        }
        public static Movement DownRight()
        {
            return new Movement(1, 1, 1);
        }
        public static List<Movement> KnightMovement()
        {
            List<Movement> movements = new List<Movement>();

            movements.Add(new Movement(-2, -1, 1));
            movements.Add(new Movement(-2, 1, 1));
            movements.Add(new Movement(-1, -2, 1));
            movements.Add(new Movement(-1, 2, 1));
            movements.Add(new Movement(1, -2, 1));
            movements.Add(new Movement(1, 2, 1));
            movements.Add(new Movement(2, -1, 1));
            movements.Add(new Movement(2, 1, 1));

            return movements;
        }

        public static Movement Pawn2Up()
        {
            return new Movement(-2, 0, 1);
        }
        public static Movement Pawn2Down()
        {
            return new Movement(2, 0, 1);
        }

        public static Movement PawnKillUpRight()
        {
            return Movement.UpRight();
        }
        public static Movement PawnKillUpLeft()
        {
            return Movement.UpLeft();
        }

        public static Movement PawnKillDownRight()
        {
            return Movement.DownRight();
        }
        public static Movement PawnKillDownLeft()
        {
            return Movement.DownLeft();
        }
        public static Movement LeftCastling()
        {
            return new Movement(0,-2,1);
        }
        public static Movement RightCastling()
        {
            return new Movement(0,2,1);
        }
    }
}
