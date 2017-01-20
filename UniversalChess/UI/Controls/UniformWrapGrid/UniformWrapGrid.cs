using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalChess.UI.Controls
{
    public class UniformWrapGrid : Panel
    {
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(UniformWrapGrid), new PropertyMetadata(1, LayoutPropertyChanged));

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(int), typeof(UniformWrapGrid), new PropertyMetadata(1, LayoutPropertyChanged));

        private static void LayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UniformWrapGrid grid = d as UniformWrapGrid;
            grid?.InvalidateMeasure();
            grid?.InvalidateArrange();
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double childWidth = availableSize.Width / Columns;
            double childHeight = availableSize.Height / Rows;

            foreach (UIElement child in Children)
            {
                child.Measure(new Size(childWidth, childHeight));
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double childWidth = finalSize.Width / Columns;
            double childHeight = finalSize.Height / Rows;

            Point leftTopAnchor = new Point(0, 0);
            Point rightBottomAnchor = new Point(childWidth, childHeight);

            int row = 0;
            int col = 0;

            foreach (UIElement child in Children)
            {
                child.Arrange(new Rect(RoundPoint(leftTopAnchor), RoundPoint(rightBottomAnchor)));
                leftTopAnchor.X += childWidth;
                rightBottomAnchor.X += childWidth;
                col++;
                if (col == Columns)
                {
                    leftTopAnchor.X = 0;
                    rightBottomAnchor.X = childWidth;
                    leftTopAnchor.Y += childHeight;
                    rightBottomAnchor.Y += childHeight;
                    row++;
                    col = 0;
                }
            }

            return finalSize;
        }

        private Point RoundPoint(Point p)
        {
            return new Point((int)(p.X + 0.5), (int)(p.Y + 0.5));
        }

        private Size RoundSize(Size s)
        {
            return new Size((int)(s.Width + 0.5), (int)(s.Height + 0.5));
        }
    }
}
