using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI.Xaml.Data;

namespace UniversalChess.UI.Controls
{
    public class ChessPieceToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Piece piece)
            {
                if (piece.Color == PieceColor.White)
                {
                    switch (piece.Type)
                    {
                        case PieceType.King: return '\u2654';
                        case PieceType.Queen: return '\u2655';
                        case PieceType.Rook: return '\u2656';
                        case PieceType.Bishop: return '\u2657';
                        case PieceType.Knight: return '\u2658';
                        case PieceType.Pawn: return '\u2659';
                    }
                }
                else
                {
                    switch (piece.Type)
                    {
                        case PieceType.King: return '\u265A';
                        case PieceType.Queen: return '\u265B';
                        case PieceType.Rook: return '\u265C';
                        case PieceType.Bishop: return '\u265D';
                        case PieceType.Knight: return '\u265E';
                        case PieceType.Pawn: return '\u265F';
                    }
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
