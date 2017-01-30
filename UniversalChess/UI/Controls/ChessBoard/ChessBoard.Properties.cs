using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI.Xaml;

namespace UniversalChess.UI.Controls
{
    public partial class ChessBoard
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(PieceColor), typeof(ChessBoard),
                new PropertyMetadata(PieceColor.White, OnOrientationChanged));

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChessBoard self = d as ChessBoard;

            PieceColor oldOrientaiton = (PieceColor)e.OldValue;
            PieceColor newOrientation = (PieceColor)e.NewValue;

            if (oldOrientaiton != newOrientation)
            {
                self._squares = self._squares.Reverse().ToArray();
                self.ItemsSource = self._squares;
            }
        }

        public PieceColor Orientation
        {
            get { return (PieceColor)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }
}
