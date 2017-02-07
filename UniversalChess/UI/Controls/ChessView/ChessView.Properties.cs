using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace UniversalChess.UI.Controls
{
    public partial class ChessView
    {
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(Position), typeof(ChessView),
                new PropertyMetadata(Position.Starting, 
                    (d, o) => ((ChessView)d).UpdatePosition()));

        public static readonly DependencyProperty LightBrushProperty =
            DependencyProperty.Register(nameof(LightBrush), typeof(Brush), typeof(ChessView),
                new PropertyMetadata(null,
                    (d, o) => ((ChessView)d).UpdateColors()));

        public static readonly DependencyProperty DarkBrushProperty =
            DependencyProperty.Register(nameof(DarkBrush), typeof(Brush), typeof(ChessView),
                new PropertyMetadata(null,
                    (d, o) => ((ChessView)d).UpdateColors()));

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(PieceColor), typeof(ChessView),
                new PropertyMetadata(PieceColor.White,
                    (d, o) => ((ChessView)d).UpdateOrientation()));

        public static readonly DependencyProperty CoordinateVisibilityProperty =
            DependencyProperty.Register(nameof(CoordinateVisibility), typeof(Visibility), typeof(ChessView),
                new PropertyMetadata(Visibility.Visible,
                    (d, o) => ((ChessView)d).UpdateCoordinateVisibility()));

        public Position Position
        {
            get { return (Position)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public Brush LightBrush
        {
            get { return (Brush)GetValue(LightBrushProperty); }
            set { SetValue(LightBrushProperty, value); }
        }

        public Brush DarkBrush
        {
            get { return (Brush)GetValue(DarkBrushProperty); }
            set { SetValue(DarkBrushProperty, value); }
        }

        public PieceColor Orientation
        {
            get { return (PieceColor)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public Visibility CoordinateVisibility
        {
            get { return (Visibility)GetValue(CoordinateVisibilityProperty); }
            set { SetValue(CoordinateVisibilityProperty, value); }
        }
    }
}
