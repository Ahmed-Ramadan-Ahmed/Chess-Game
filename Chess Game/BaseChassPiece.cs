using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    enum Types
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }
    enum Weights
    {
        Pawn = 1,
        Rook = 5,
        Knight = 3,
        Bishop = 3,
        Queen = 9,
        King = 100
    }
    enum Symbols
    {
        blackpawn = '♟',
        blackrook = '♜',
        blackknight = '♞',
        blackbishop = '♝',
        blackqueen = '♛',
        blackking = '♚',

        whitepawn = '♙',
        whiterook = '♖',
        whiteknight = '♘',
        whitebishop = '♗',
        whitequeen = '♕',
        whiteking = '♔'
    }

    internal abstract class BaseChassPiece
    {
        public readonly Colors Color;
        public readonly Types Type;
        public readonly Weights Weight;
        public readonly Symbols Symbole;
        public readonly List<Movement> Movements;
        public bool IsDead { get; private set; }
        public BaseChassPiece(Colors _color, Types _type)
        {
            Color = _color;
            Type = _type;
            IsDead = false;
            
            Movements = new List<Movement>();

            switch (Type)
            {
                case Types.Pawn:
                    Symbole = Color == Colors.White ? Symbols.whitepawn : Symbols.blackpawn;
                    Weight = Weights.Pawn;
                    break;
                case Types.Rook:
                    Symbole = Color == Colors.White ? Symbols.whiterook : Symbols.blackrook;
                    Weight = Weights.Rook;
                    break;
                case Types.Knight:
                    Symbole = Color == Colors.White ? Symbols.whiteknight : Symbols.blackknight;
                    Weight = Weights.Knight;
                    break;
                case Types.Bishop:
                    Symbole = Color == Colors.White ? Symbols.whitebishop : Symbols.blackbishop;
                    Weight = Weights.Bishop;
                    break;
                case Types.Queen:
                    Symbole = Color == Colors.White ? Symbols.whitequeen : Symbols.blackqueen;
                    Weight = Weights.Queen;
                    break;
                case Types.King:
                    Symbole = Color == Colors.White ? Symbols.whiteking : Symbols.blackking;
                    Weight = Weights.King;
                    break;
                default:
                    throw new ArgumentException("Invalid piece type");
            }

        }

        public event EventHandler<ScoreEventArgs> ? Death;
        protected virtual void OnDeath(ScoreEventArgs e)
        {
            Death?.Invoke(this, e);
        }
        public bool Kill()
        {
            if(IsDead) return false;
            
            IsDead = true;

            OnDeath(new ScoreEventArgs((int)Weight));

            return true;
        }
        public override string ToString() => $"Piece: {Type}, Color: {Color}, Weight: {Weight}";
    }
}
