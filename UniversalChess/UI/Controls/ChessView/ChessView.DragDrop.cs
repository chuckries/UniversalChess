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
using Windows.UI.Xaml.Media.Imaging;

namespace UniversalChess.UI.Controls
{
    public partial class ChessView
    {
        private ChessViewItem _draggedItem;
        private ChessViewItem DraggedItem
        {
            get { return _draggedItem; }
            set
            {
                if (value == null)
                {
                    if (_draggedItem != null)
                    {
                        VisualStateManager.GoToState(_draggedItem, "NotSelected", true);
                        VisualStateManager.GoToState(_draggedItem, "NotDragging", true);
                    }
                }
                else
                {
                    VisualStateManager.GoToState(value, "Selected", true);
                    VisualStateManager.GoToState(value, "Dragging", true);
                }
                _draggedItem = value;
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
                    Model.Position position = new Model.Position(Position);
                    position[item.Id] = DraggedItem.Piece;
                    position[DraggedItem.Id] = null;
                    Position = position;
                    e.AcceptedOperation = DataPackageOperation.Move;
                }
            }
        }
    }
}
