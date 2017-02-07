using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalChess.Model
{
    public class Position : List<Piece>
    {
        public Position() : base(new Piece[64])
        {
        }

        public Position(Position position) : base(position)
        {
        }

        public Position(string position) : this()
        {
            int i = 0;
            foreach (char c in position)
            {
                switch (c)
                {
                    case 'p': this[i++] = new Pawn      (PieceColor.Black); break;
                    case 'n': this[i++] = new Knight    (PieceColor.Black); break;
                    case 'b': this[i++] = new Bishop    (PieceColor.Black); break;
                    case 'r': this[i++] = new Rook      (PieceColor.Black); break;
                    case 'q': this[i++] = new Queen     (PieceColor.Black); break;
                    case 'k': this[i++] = new King      (PieceColor.Black); break;
                    case 'P': this[i++] = new Pawn      (PieceColor.White); break;
                    case 'N': this[i++] = new Knight    (PieceColor.White); break;
                    case 'B': this[i++] = new Bishop    (PieceColor.White); break;
                    case 'R': this[i++] = new Rook      (PieceColor.White); break;
                    case 'Q': this[i++] = new Queen     (PieceColor.White); break;
                    case 'K': this[i++] = new King      (PieceColor.White); break;
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8': i += int.Parse(c.ToString()); break;
                    case '/': if (i % 8 != 0) throw new ArgumentException("Invalid FEN"); break;
                    default: throw new ArgumentException($"Invalid fen character: '{c}'");
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            int perRowCount = 0;
            int emptySquareCount = 0;

            Action doEmptySquares = () =>
            {
                if (emptySquareCount > 0)
                {
                    builder.Append(emptySquareCount.ToString());
                    emptySquareCount = 0;
                }
            };

            foreach (Piece piece in this)
            {
                if (piece == null)
                {
                    emptySquareCount++;
                }
                else
                {
                    doEmptySquares();
                    char pieceChar = 'x';
                    switch(piece.Type)
                    {
                        case PieceType.Pawn:    pieceChar = 'P'; break;
                        case PieceType.Knight:  pieceChar = 'N'; break;
                        case PieceType.Bishop:  pieceChar = 'B'; break;
                        case PieceType.Rook:    pieceChar = 'R'; break;
                        case PieceType.Queen:   pieceChar = 'Q'; break;
                        case PieceType.King:    pieceChar = 'K'; break;
                        default: throw new ArgumentException("Unknown piece type in position");
                    }
                    if (piece.Color == PieceColor.Black)
                    {
                        pieceChar = char.ToLower(pieceChar);
                    }
                    builder.Append(pieceChar);
                }

                perRowCount++;
                if (perRowCount == 8)
                {
                    doEmptySquares();
                    perRowCount = 0;
                    builder.Append('/');
                }
            }

            builder.Remove(builder.Length - 1, 1); // remove final slash
            return builder.ToString();
        }

        // Does not modify this object, returns a new position
        public Position MakeMove(int sourceId, int destId)
        {
            Piece piece = this[sourceId];
            if (piece != null)
            {
                Position newPosition = new Position(this);
                newPosition[destId] = this[sourceId];
                newPosition[sourceId] = null;
                return newPosition;
            }

            return this;
        }

        public static Position Empty { get { return new Position(); } }
        public static Position Starting { get { return new Position("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR"); } }
        public static Position RuyLopez { get { return new Position("r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R"); } }
    }
}
