using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    internal class ChessBoard
    {
        public BoardSquareCell[,] Board;
        public const int Size = 8;
        public ChessBoard()
        {
            InitializeBoard();
            InitializePieces();
        }
        private ChessBoard(BoardSquareCell[,] chessBoard)
        {
            Board = new BoardSquareCell[Size + 1, Size + 1];
            for (int row = 1; row <= Size; row++)
            {
                for (int col = 1; col <= Size; col++)
                {
                    Board[row, col] = new BoardSquareCell(row, col);
                    Board[row, col].Piece = chessBoard[row, col].Piece;
                }
            } 
        }

        private void InitializeBoard()
        {
            Board = new BoardSquareCell[Size+1, Size+1];
            for (int row = 1; row <= Size; row++)
            {
                for (int col = 1; col <= Size; col++)
                {
                    Board[row, col] = new BoardSquareCell(row, col);
                }
            }
        }
        private void InitializePieces()
        {
            for (int col = 1; col <= Size; col++)
            {
                Board[2, col].Piece = new Pawn(Colors.White);
                Board[7, col].Piece = new Pawn(Colors.Black);
            }

            Board[1, 1].Piece = new Rook(Colors.White);
            Board[1, 8].Piece = new Rook(Colors.White);
            Board[8, 1].Piece = new Rook(Colors.Black);
            Board[8, 8].Piece = new Rook(Colors.Black);

            Board[1, 2].Piece = new Knight(Colors.White);
            Board[1, 7].Piece = new Knight(Colors.White);
            Board[8, 2].Piece = new Knight(Colors.Black);
            Board[8, 7].Piece = new Knight(Colors.Black);

            Board[1, 3].Piece = new Bishop(Colors.White);
            Board[1, 6].Piece = new Bishop(Colors.White);
            Board[8, 3].Piece = new Bishop(Colors.Black);
            Board[8, 6].Piece = new Bishop(Colors.Black);

            Board[1, 4].Piece = new Queen(Colors.White);
            Board[8, 4].Piece = new Queen(Colors.Black);

            Board[1, 5].Piece = new King(Colors.White);
            Board[8, 5].Piece = new King(Colors.Black);
        }
        public void printBoard()
        {
            Console.Write("   ");
            for (int i = 1; i <= Size; i++)
            {
                Console.Write(" " + (char)(i+'A'-1) + " ");
            }

            Console.WriteLine();
            for (int row = 1; row <= Size; row++)
            {
                Console.Write(row + "  ");
                for (int col = 1; col <= Size; col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write(Board[row, col].Content);
                    Console.ResetColor(); 
                }
                Console.WriteLine();
            }
        }
        public BoardSquareCell GetCell(string name)
        {
            if (name == null || name.Length != 2)
            {
                return null;
            }

            int col = name[0] - 'A' + 1;
            int row = name[1] - '0';

            if (col < 1 || col > 8 || row < 1 || row > 8)
            {
                return null;
            }

            return Board[row, col];
        }
        public bool vaildFromCell(Colors playerColor, string from)
        {

            BoardSquareCell? cell = GetCell(from);

            if (cell == null) return false;
            if (cell.IsEmpty) return false;
            if (cell.Piece.Color != playerColor) return false;

            return true;
        }
        public bool vaildToCell(Colors playerColor, string to)
        {

            BoardSquareCell cell = GetCell(to);

            if (cell == null) return false;
            if (!cell.IsEmpty && cell.Piece?.Color == playerColor) return false;

            return true;
        }
        public bool Move(Colors playerColor, string from, string to)
        {
            if (!vaildFromCell(playerColor,from)) return false;
            if (!vaildToCell(playerColor,to)) return false;

            BoardSquareCell fromCell = GetCell(from);
            BoardSquareCell toCell = GetCell(to);

            if (!CanMove(fromCell, toCell)) return false;

            if(IsCheckMate(playerColor, fromCell.Position, toCell.Position))
            {
                return false;
            }

            MovePiece(fromCell, toCell);
            return true;
        }
        public bool IsThearten(Position target, Colors playerColor)
        {
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    if (Board[i, j].Piece == null) continue;
                    if (Board[i, j].Piece.Color == playerColor) continue;
                    if (CanMove(Board[i, j], Board[target.Row, target.Column]))
                        return true;
                }
            }
            return false;
        }
        public Position GetKingPosition(Colors playerColor)
        {
            Position target = null;
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    if (Board[i, j].Piece == null) continue;
                    if (Board[i, j].Piece.Color != playerColor) continue;
                    if (Board[i, j].Piece.Type == Types.King)
                    {
                        target = new Position(i, j);
                        break;
                    }
                }
            }
            return target;
        }
        private bool IsCheckMate(Colors playerColor, Position from, Position to)
        {
            ChessBoard CopyChessBoard = new ChessBoard(Board);

            CopyChessBoard.MovePiece(CopyChessBoard.Board[from.Row, from.Column] , CopyChessBoard.Board[to.Row, to.Column]);

            Position? target = CopyChessBoard.GetKingPosition(playerColor);

            if (CopyChessBoard.IsThearten(target, playerColor))
                return true;

            return false;
        }
        private BaseChassPiece Promote(Colors color)
        {
            while(true)
            {
                Console.WriteLine("1. Queen");
                Console.WriteLine("2. Rook");
                Console.WriteLine("3. Bishop");
                Console.WriteLine("4. Knight");
                Console.Write("Promote to: ");
                string input = Console.ReadLine();
                if(int.TryParse(input, out int x) && x <= 4 && x >= 1)
                {
                    switch(x)
                    {
                        case 1:
                            return new Queen(color);
                        case 2:
                            return new Rook(color);
                        case 3:
                            return new Bishop(color);
                        case 4:
                            return new Knight(color);
                    }
                }
            }
        }
        private void MovePiece(BoardSquareCell from, BoardSquareCell to)
        {
            if(from.Piece.Type == Types.Pawn)
            {
                if( (to.Position.Row == 1 && from.Piece.Color == Colors.Black) ||
                    (to.Position.Row == 8 && from.Piece.Color == Colors.White) )
                {   
                    var NewPiece = Promote(from.Piece.Color);
                    ((Pawn)from.Piece).OnPromotion(NewPiece);
                    from.Piece = NewPiece;
                }
            }

            if(from.Piece.Type == Types.King)
            {
                if(Math.Abs(from.Position.Column - to.Position.Column) == 2) 
                    DoCastling(to);
            }
            
            to.Piece = from.Piece;
            from.Piece = null;
        }
        private void DoCastling(BoardSquareCell to)
        {
            if (to.Position.Column == 7)
            {
                MovePiece(Board[to.Position.Row, 8], Board[to.Position.Row, 6]);
            }
            else if (to.Position.Column == 3)
            {
                MovePiece(Board[to.Position.Row, 1], Board[to.Position.Row, 4]);
            }
        }

        private bool Search(Position last, Position to, Position Change, int limit)
        {
            Position current = new Position(last.Row + Change.Row, last.Column + Change.Column);
            limit--;

            if (current == to) return true;

            if (current.Row < 1 || current.Row > 8 || current.Column < 1 || current.Column > 8)
                return false;

            if (!Board[current.Row, current.Column].IsEmpty) return false;

            if (limit == 0) return false;

            return Search(current, to, Change,limit);
        }

        private bool CanMove(BoardSquareCell fromCell, BoardSquareCell toCell)
        {
            BaseChassPiece? piece = fromCell.Piece;
            List<Movement> movements = piece?.Movements ?? new List<Movement>();
            foreach (var movement in movements) 
            {
                if (Search(fromCell.Position, toCell.Position, new Position(movement.RowChange,movement.ColumnChange), movement.MovesLimit))
                {
                    return true;
                }
            }

            if(piece.Type == Types.Pawn)
            {
                if(IsPawnFirstMove((Pawn)piece,fromCell.Position))
                {
                    Movement movement;
                    if (piece.Color == Colors.White)
                    {
                        movement = Movement.Pawn2Down();
                    }
                    else
                    {
                        movement = Movement.Pawn2Up();
                    }

                    if(Search(fromCell.Position, toCell.Position, new Position(movement.RowChange, movement.ColumnChange), movement.MovesLimit))
                    {
                        return true;
                    }
                }

                if(IsPawnCanKillLeft(fromCell.Position))
                {
                    Movement movement = piece.Color == Colors.White ? Movement.PawnKillDownLeft() : Movement.PawnKillUpLeft();
                    if (Search(fromCell.Position, toCell.Position, new Position(movement.RowChange, movement.ColumnChange), movement.MovesLimit))
                    {
                        return true;
                    }
                }

                if (IsPawnCanKillRight(fromCell.Position))
                {
                    Movement movement = piece.Color == Colors.White ? Movement.PawnKillDownRight() : Movement.PawnKillUpRight();
                    if (Search(fromCell.Position, toCell.Position, new Position(movement.RowChange, movement.ColumnChange), movement.MovesLimit))
                    {
                        return true;
                    }
                }
            }

            if(piece.Type == Types.King)
            {
                if(CanLeftCastling(piece.Color))
                {
                    Movement movement = Movement.LeftCastling();
                    if (Search(fromCell.Position, toCell.Position, new Position(movement.RowChange, movement.ColumnChange), movement.MovesLimit))
                    {
                        return true;
                    }
                }
                if (CanRightCastling(piece.Color))
                {
                    Movement movement = Movement.RightCastling();
                    if (Search(fromCell.Position, toCell.Position, new Position(movement.RowChange, movement.ColumnChange), movement.MovesLimit))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsPawnCanKillLeft(Position PawnPosition)
        {
            Colors color = Board[PawnPosition.Row , PawnPosition.Column].Piece.Color;
            
            if(color == Colors.White)
            {
                Position newPosition = new Position(PawnPosition.Row+1 , PawnPosition.Column-1);
                if(newPosition.Row > 8 ||  newPosition.Column < 1) return false;
                if (Board[newPosition.Row , newPosition.Column].IsEmpty) return false;
                if (Board[newPosition.Row , newPosition.Column].Piece.Color == color) return false;
            }
            else
            {
                Position newPosition = new Position(PawnPosition.Row - 1, PawnPosition.Column - 1);
                if (newPosition.Row < 1 || newPosition.Column < 1) return false;
                if (Board[newPosition.Row, newPosition.Column].IsEmpty) return false;
                if (Board[newPosition.Row, newPosition.Column].Piece.Color == color) return false;
            }

            return true;
        }

        private bool IsPawnCanKillRight(Position PawnPosition)
        {
            Colors color = Board[PawnPosition.Row, PawnPosition.Column].Piece.Color;

            if (color == Colors.White)
            {
                Position newPosition = new Position(PawnPosition.Row + 1, PawnPosition.Column + 1);
                if (newPosition.Row > 8 || newPosition.Column > 8) return false;
                if (Board[newPosition.Row, newPosition.Column].IsEmpty) return false;
                if (Board[newPosition.Row, newPosition.Column].Piece.Color == color) return false;
            }
            else
            {
                Position newPosition = new Position(PawnPosition.Row - 1, PawnPosition.Column + 1);
                if (newPosition.Row < 1 || newPosition.Column > 8) return false;
                if (Board[newPosition.Row, newPosition.Column].IsEmpty) return false;
                if (Board[newPosition.Row, newPosition.Column].Piece.Color == color) return false;
            }

            return true;
        }

        private bool IsPawnFirstMove(Pawn pawn, Position position)
        {
            return pawn.Color == Colors.White ? position.Row == 2 : position.Row == 7;
        }
        private bool CanLeftCastling(Colors color)
        {
            BaseChassPiece rook;
            BaseChassPiece king;

            if(color == Colors.White)
            {
                rook = Board[1, 1].Piece;
                king = Board[1, 5].Piece;
            }
            else
            {
                rook = Board[8, 1].Piece;
                king = Board[8, 5].Piece;
            }

            if (rook == null || king == null) return false;
            if (rook.Type != Types.Rook || king.Type != Types.King) return false;
            if (rook.Color != color || king.Color != color) return false;

            /******/
            int row = color == Colors.White ? 1 : 8;
            for (int col = 2; col < 5; col++)
            {
                if (!Board[row, col].IsEmpty) return false;
                if(IsThearten(new Position(row, col), color)) return false;
            }

            return true;
        }
        private bool CanRightCastling(Colors color)
        {
            BaseChassPiece rook;
            BaseChassPiece king;
            if (color == Colors.White)
            {
                rook = Board[1, 8].Piece;
                king = Board[1, 5].Piece;
            }
            else
            {
                rook = Board[8, 8].Piece;
                king = Board[8, 5].Piece;
            }
            if (rook == null || king == null) return false;
            if (rook.Type != Types.Rook || king.Type != Types.King) return false;
            if (rook.Color != color || king.Color != color) return false;

            /******/
            int row = color == Colors.White ? 1 : 8;
            for (int col = 6; col < 8; col++)
            {
                if (!Board[row, col].IsEmpty) return false;
                if (IsThearten(new Position(1, col), color)) return false;
            }

            return true;
        }
        public bool CanMoveAny(Colors color)
        {
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    var cell1 = Board[i, j];
                    if (cell1.Piece == null) continue;
                    if (cell1.Piece.Color != color) continue;

                    for (int k = 1; k <= Size; k++)
                    {
                        for (int l = 1; l <= Size; l++)
                        {
                            var cell2 = Board[k, l];
                            if (!cell2.IsEmpty && cell2.Piece.Color == color) continue;
                            if (CanMove(cell1, cell2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void Subscribe(Player player)
        {
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    var cell = Board[i, j];
                    if (cell.IsEmpty) continue;
                    if (cell.Piece.Color != player.Color)
                    {
                        cell.Piece.Death += player.AddScore;

                        if(cell.Piece.Type == Types.Pawn)
                        {
                            ((Pawn)cell.Piece).Promotion += player.SubtractScore;
                        }
                    }
                }
            }
        }
    }
}
