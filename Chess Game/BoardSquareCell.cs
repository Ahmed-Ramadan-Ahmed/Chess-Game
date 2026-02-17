using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    enum Colors { White , Black }
    public class Position
    {
        public int Row { get; }
        public int Column { get; }
        public Position(int row, int col)
        {
            Row = row;
            Column = col;
        }
        public static bool operator ==(Position position1 , Position position2)
        {
            return position1.Row == position2.Row && position1.Column == position2.Column;
        }
        public static bool operator !=(Position position1, Position position2)
        {
            return position1.Row != position2.Row || position1.Column != position2.Column;
        }
    }
    internal class BoardSquareCell
    {
        public string Content => _piece == null ? "   " : " " + (char)_piece.Symbole + " ";
        public readonly Position Position;
        public readonly Colors Color;
        private BaseChassPiece? _piece;
        public bool IsEmpty { get => _piece == null; }
        public BaseChassPiece? Piece
        {
            get => _piece;
            set
            {
                if(value == null)
                {
                    _piece = null;
                    return;
                }

                if (!IsEmpty)
                {
                    _piece.Kill();
                }
                _piece = value;
            }
        }
        public BoardSquareCell(int row, int col)
        {
            Position = new Position(row, col);
            Color = (row + col) % 2 == 0 ? Colors.White : Colors.Black;
            _piece = null;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            BoardSquareCell cell = (BoardSquareCell)obj;
            return cell.Position == Position;
        }

        public static bool operator == (BoardSquareCell? cell1, BoardSquareCell? cell2)
        {
            if(cell1 is null)
            {
                return cell2 is null;
            }

            return cell1.Equals(cell2);
        }
        public static bool operator != (BoardSquareCell? cell1, BoardSquareCell? cell2)
        {
            if (cell1 == null) return false;

            return ! cell1.Equals(cell2);
        }
        public override string ToString() => $"{Content}";

    }
}
