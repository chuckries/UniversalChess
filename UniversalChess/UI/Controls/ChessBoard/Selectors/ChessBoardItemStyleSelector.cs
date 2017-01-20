using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalChess.UI.Controls
{
    class ChessBoardItemStyleSelector : StyleSelector
    {
        public Style LightSquareStyle { get; set; }
        public Style DarkSquareStyle { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            ChessSquare chessSquare = item as ChessSquare;
            if (chessSquare != null)
            {
                int row = chessSquare.Id / 8;
                int col = chessSquare.Id % 8;

                return (row + col) % 2 == 0 ? LightSquareStyle : DarkSquareStyle;
            }

            return base.SelectStyleCore(item, container);
        }
    }
}
