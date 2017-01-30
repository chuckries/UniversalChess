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
    }

    public class Pawn : Piece
    {
        public Pawn(PieceColor color) : base(color)
        {
            Type = PieceType.Pawn;
        }
    }

    public class Knight : Piece
    {
        public Knight(PieceColor color) : base(color)
        {
            Type = PieceType.Knight;
        }
    }

    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color)
        {
            Type = PieceType.Bishop;
        }
    }

    public class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
            Type = PieceType.Rook;
        }
    }

    public class Queen : Piece
    {
        public Queen(PieceColor color) : base(color)
        {
            Type = PieceType.Queen;
        }
    }

    public class King : Piece
    {
        public King(PieceColor color) : base(color)
        {
            Type = PieceType.King;
        }
    }

}
