using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UniversalChess.UI.Controls
{
    [TemplatePart(Name = ChessGrid_PART, Type = typeof(Grid))]
    public partial class ChessView : Control
    {
        public ChessView()
        {
            DefaultStyleKey = typeof(ChessView);

            for (int i = 0; i < _containers.Length; i++)
            {
                ChessViewItem item = new ChessViewItem(i);
                item.Tapped += Item_Tapped;
                item.DragStarting += Item_DragStarting;
                item.DropCompleted += Item_DropCompleted;
                item.DragEnter += Item_DragEnter;
                item.DragOver += Item_DragOver;
                item.DragLeave += Item_DragLeave;
                item.Drop += Item_Drop;
                item.AllowDrop = true;
                _containers[i] = item;
            }
        }

        private ChessViewItem _selectedItem;
        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is ChessViewItem tappedItem)
            {
                tappedItem.Focus(FocusState.Programmatic);

                if (tappedItem.Piece == null)
                {
                    return;
                }
                if (_selectedItem == tappedItem)
                {
                    VisualStateManager.GoToState(tappedItem, "NotSelected", true);
                    _selectedItem = null;
                }
                else
                {
                    if (_selectedItem != null)
                    {
                        VisualStateManager.GoToState(_selectedItem, "NotSelected", true);
                    }
                    VisualStateManager.GoToState(tappedItem, "Selected", true);
                    _selectedItem = tappedItem;
                }

                e.Handled = true;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _chessGrid = (Grid)GetTemplateChild(ChessGrid_PART);
            CreateGrid();
            UpdateOrientation();
            UpdateCoordinateVisibility();
            UpdateColors();
            UpdatePosition();
        }

        private void CreateGrid()
        {
            if (_chessGrid != null)
            {
                _chessGrid.Children.Clear();
                _chessGrid.RowDefinitions.Clear();
                _chessGrid.ColumnDefinitions.Clear();
                for (int i = 0; i < 8; i++)
                {
                    _chessGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    _chessGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
                foreach(ChessViewItem container in _containers)
                {
                    _chessGrid.Children.Add(container);
                }
            }
        }

        private void UpdateOrientation()
        {
            var enumerable = (Orientation == PieceColor.White) ? _containers : _containers.Reverse();

            int i = 0;
            foreach (ChessViewItem item in enumerable)
            {
                Grid.SetRow(item, i / 8);
                Grid.SetColumn(item, i % 8);
                i++;
            }
            if (CoordinateVisibility == Visibility.Visible)
            {
                UpdateCoordinateVisibility();
            }
        }

        private void UpdateColors()
        {
            foreach(ChessViewItem container in _containers)
            {
                bool isLightSquare = ((container.Id / 8) + (container.Id % 8)) % 2 == 0;
                container.Background = isLightSquare ? LightBrush : DarkBrush;
                container.Foreground = isLightSquare ? DarkBrush : LightBrush;
            }
        }

        private void UpdateCoordinateVisibility()
        {
            foreach (ChessViewItem container in _containers)
            {
                container.RankVisibility = Grid.GetColumn(container) == 0 ? CoordinateVisibility : Visibility.Collapsed;
                container.FileVisibility = Grid.GetRow(container) == 7 ? CoordinateVisibility : Visibility.Collapsed;
            }
        }

        private void UpdatePosition()
        {
            if (Position == null)
            {
                foreach (ChessViewItem container in _containers)
                {
                    container.Piece = null;
                    container.CanDrag = false;
                }
            }
            else
            {
                for (int i = 0; i < _containers.Length && i < Position.Count; i++)
                {
                    ChessViewItem container = _containers[i];
                    container.Piece = Position[i];
                    container.CanDrag = container.Piece != null;
                }
            }
        }

        // Template parts
        private const string ChessGrid_PART = "ChessGrid";
        private Grid _chessGrid;

        // Private data
        private ChessViewItem[] _containers = new ChessViewItem[64];
    }
}
