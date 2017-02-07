using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace UniversalChess.UI.Controls
{
    public partial class ChessView
    {
        private ChessViewItem _draggedItem;
        private ChessViewItem _selectedItem;
        private ChessViewItem DraggedItem
        {
            get { return _draggedItem; }
            set
            {
                if (_draggedItem != value)
                {
                    if (value != null)
                    {
                        VisualStateManager.GoToState(value, "Dragging", true);
                    }
                    if (_draggedItem != null)
                    {
                        VisualStateManager.GoToState(_draggedItem, "NotDragging", true);
                    }
                    _draggedItem = value;
                    SelectedItem = value;
                }
            }
        }

        private ChessViewItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    if (value != null)
                    {
                        VisualStateManager.GoToState(value, "Selected", true);
                    }
                    if (_selectedItem != null)
                    {
                        VisualStateManager.GoToState(_selectedItem, "NotSelected", true);
                    }
                    _selectedItem = value;
                }
            }
        }

        private void Item_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is ChessViewItem item)
            {
                if (SelectedItem == null)
                {
                    if (Position[item.Id] != null)
                    {
                        item.CapturePointer(e.Pointer);
                        SelectedItem = item;
                    }
                }
                else
                {
                    if (SelectedItem != item)
                    {
                        Position = Position.MakeMove(SelectedItem.Id, item.Id);
                    }
                    SelectedItem = null;
                }
            }
        }

        private async void Item_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if (sender is ChessViewItem item)
            {
                args.AllowedOperations = DataPackageOperation.Move;

                DraggedItem = item;

                FrameworkElement dragElement = item.PieceTemplateRoot;
                if (dragElement != null)
                {
                    DragOperationDeferral deferral = args.GetDeferral();
                    RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                    double scale = 1.5;
                    await renderTargetBitmap.RenderAsync(
                        dragElement,
                        (int)(dragElement.ActualWidth * scale),
                        (int)(dragElement.ActualHeight * scale)
                        );
                    IBuffer buffer = await renderTargetBitmap.GetPixelsAsync();
                    SoftwareBitmap bitmap = SoftwareBitmap.CreateCopyFromBuffer(
                        buffer,
                        BitmapPixelFormat.Bgra8,
                        renderTargetBitmap.PixelWidth,
                        renderTargetBitmap.PixelHeight
                        );
                    args.DragUI.SetContentFromSoftwareBitmap(bitmap, new Point(renderTargetBitmap.PixelWidth / 2, renderTargetBitmap.PixelHeight / 2));
                    deferral.Complete();
                }
            }
        }

        private void Item_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            if (sender is ChessViewItem item)
            {
                DraggedItem = null;
            }
        }

        private void Item_DragEnter(object sender, DragEventArgs e)
        {
            if (sender is ChessViewItem item && DraggedItem != null)
            {
                if (item != DraggedItem)
                {
                    VisualStateManager.GoToState(item, "DragTarget", true);
                }
                e.AcceptedOperation = DataPackageOperation.Move;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;
                e.Handled = true;
            }
        }

        private void Item_DragOver(object sender, DragEventArgs e)
        {
            if (sender is ChessViewItem item && DraggedItem != null)
            {
                if (item != DraggedItem)
                {
                    VisualStateManager.GoToState(item, "DragTarget", true);
                }
                e.AcceptedOperation = DataPackageOperation.Move;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;
                e.Handled = true;
            }
        }

        private void Item_DragLeave(object sender, DragEventArgs e)
        {
            if (sender is ChessViewItem item && DraggedItem != null)
            {
                if (item != DraggedItem)
                {
                    VisualStateManager.GoToState(item, "NotDragging", true);
                }
                e.AcceptedOperation = DataPackageOperation.None;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;
                e.Handled = true;
            }
        }

        private void Item_Drop(object sender, DragEventArgs e)
        {
            if (sender is ChessViewItem item && DraggedItem != null)
            {
                e.AcceptedOperation = DataPackageOperation.None;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;
                e.Handled = true;

                if (item != DraggedItem)
                {
                    VisualStateManager.GoToState(item, "NotDragging", true);
                    Position = Position.MakeMove(_draggedItem.Id, item.Id);
                    e.AcceptedOperation = DataPackageOperation.Move;
                }
            }
        }
    }
}
