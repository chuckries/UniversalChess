using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace UniversalChess.UI.Controls
{
    public partial class ChessViewItem
    {
        public static readonly DependencyProperty ChessViewProperty =
            DependencyProperty.Register(nameof(ChessView), typeof(ChessView), typeof(ChessViewItem),
                new PropertyMetadata(null));

        public static readonly DependencyProperty PieceProperty =
            DependencyProperty.Register(nameof(Piece), typeof(Piece), typeof(ChessViewItem),
                new PropertyMetadata(null));

        public static readonly DependencyProperty PieceTemplateProperty =
            DependencyProperty.Register(nameof(PieceTemplate), typeof(DataTemplate), typeof(ChessViewItem),
                new PropertyMetadata(null));

        public static readonly DependencyProperty RankVisibilityProperty =
            DependencyProperty.Register(nameof(RankVisibility), typeof(Visibility), typeof(ChessViewItem),
                new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty FileVisibilityProperty =
            DependencyProperty.Register(nameof(FileVisibility), typeof(Visibility), typeof(ChessViewItem),
                new PropertyMetadata(Visibility.Visible));

        public ChessView ChessView
        {
            get { return (ChessView)GetValue(ChessViewProperty); }
            set { SetValue(ChessViewProperty, value); }
        }

        public Piece Piece
        {
            get { return (Piece)GetValue(PieceProperty); }
            set { SetValue(PieceProperty, value); }
        }

        public DataTemplate PieceTemplate
        {
            get { return (DataTemplate)GetValue(PieceTemplateProperty); }
            set { SetValue(PieceTemplateProperty, value); }
        }

        public Visibility RankVisibility
        {
            get { return (Visibility)GetValue(RankVisibilityProperty); }
            set { SetValue(RankVisibilityProperty, value); }
        }

        public Visibility FileVisibility
        {
            get { return (Visibility)GetValue(FileVisibilityProperty); }
            set { SetValue(FileVisibilityProperty, value); }
        }

        public FrameworkElement PieceTemplateRoot
        {
            get { return (FrameworkElement)VisualTreeHelper.GetChild(_piecePresenter, 0); }
        }
    }
}
