using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
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
            if (item is Square square)
            {
                int row = square.Id / 8;
                int col = square.Id % 8;

                return (row + col) % 2 == 0 ? LightSquareStyle : DarkSquareStyle;
            }

            return base.SelectStyleCore(item, container);
        }
    }
}
