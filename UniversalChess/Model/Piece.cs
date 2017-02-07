using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalChess.Model
{
    public abstract class Piece
    {
        public PieceType Type { get; protected set; }
        public PieceColor Color { get; protected set; }

        public Piece(PieceColor color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return Color == PieceColor.White ? "W" : "B";
        }
    }

    public class Pawn : Piece
    {
        public Pawn(PieceColor color) : base(color)
        {
            Type = PieceType.Pawn;
        }

        public override string ToString()
        {
            return base.ToString() + "P";
        }
    }

    public class Knight : Piece
    {
        public Knight(PieceColor color) : base(color)
        {
            Type = PieceType.Knight;
        }

        public override string ToString()
        {
            return base.ToString() + "N";
        }
    }

    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color)
        {
            Type = PieceType.Bishop;
        }

        public override string ToString()
        {
            return base.ToString() + "B";
        }
    }

    public class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
            Type = PieceType.Rook;
        }

        public override string ToString()
        {
            return base.ToString() + "R";
        }
    }

    public class Queen : Piece
    {
        public Queen(PieceColor color) : base(color)
        {
            Type = PieceType.Queen;
        }

        public override string ToString()
        {
            return base.ToString() + "Q";
        }
    }

    public class King : Piece
    {
        public King(PieceColor color) : base(color)
        {
            Type = PieceType.King;
        }

        public override string ToString()
        {
            return base.ToString() + "K";
        }
    }

}
