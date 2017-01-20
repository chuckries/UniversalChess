using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalChess.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace UniversalChess.UI.Controls
{
    public sealed class ChessBoard : ListView
    {
        public ChessBoard()
        {
            this.DefaultStyleKey = typeof(ChessBoard);

            ChessSquare[] squares = new ChessSquare[64];
            for (int i = 0; i < squares.Length; i++)
            {
                squares[i] = new ChessSquare(i, null);
            }

            squares[0].Piece = new Piece(PieceType.King, PieceColor.White);
            squares[1].Piece = new Piece(PieceType.King, PieceColor.Black);

            ItemsSource = squares;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ChessBoardItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
