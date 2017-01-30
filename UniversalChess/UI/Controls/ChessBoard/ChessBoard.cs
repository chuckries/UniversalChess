using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace UniversalChess.UI.Controls
{
    public sealed partial class ChessBoard : ItemsControl
    {
        private Square[] _squares = new Square[64];

        public ChessBoard()
        {
            this.DefaultStyleKey = typeof(ChessBoard);

            Piece[] pieces = new Piece[]
            {
                new Rook    (PieceColor.Black),
                new Knight  (PieceColor.Black),
                new Bishop  (PieceColor.Black),
                new Queen   (PieceColor.Black),
                new King    (PieceColor.Black),
                new Bishop  (PieceColor.Black),
                new Knight  (PieceColor.Black),
                new Rook    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                new Pawn    (PieceColor.Black),
                null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, null,
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Pawn    (PieceColor.White),
                new Rook    (PieceColor.White),
                new Knight  (PieceColor.White),
                new Bishop  (PieceColor.White),
                new Queen   (PieceColor.White),
                new King    (PieceColor.White),
                new Bishop  (PieceColor.White),
                new Knight  (PieceColor.White),
                new Rook    (PieceColor.White)
            };

            for (int i = 0; i < pieces.Length; i++)
            {
                _squares[i] = new Square { Id = i, Piece = pieces[i] };
            }

            ItemsSource = _squares;
        }

        public void FlipBoard()
        {
            if (Orientation == PieceColor.White)
            {
                Orientation = PieceColor.Black;
            }
            else
            {
                Orientation = PieceColor.White;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ChessBoardItem();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            if (element is ChessBoardItem chessBoardItem && item is Square square)
            {
                chessBoardItem.PointerPressed += ChessBoardItem_PointerPressed;

                chessBoardItem.DragStarting += ChessBoardItem_DragStarting;
                chessBoardItem.DropCompleted += ChessBoardItem_DropCompleted;
                chessBoardItem.DragEnter += ChessBoardItem_DragEnter;
                chessBoardItem.DragOver += ChessBoardItem_DragOver;
                chessBoardItem.DragLeave += ChessBoardItem_DragLeave;
                chessBoardItem.Drop += ChessBoardItem_Drop;

                chessBoardItem.CanDrag = (square.Piece != null);
                chessBoardItem.AllowDrop = true;
            }
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            if (element is ChessBoardItem chessBoardItem)
            {
                chessBoardItem.PointerPressed -= ChessBoardItem_PointerPressed;
                
                chessBoardItem.DragStarting -= ChessBoardItem_DragStarting;
                chessBoardItem.DropCompleted -= ChessBoardItem_DropCompleted;
                chessBoardItem.DragEnter -= ChessBoardItem_DragEnter;
                chessBoardItem.DragOver -= ChessBoardItem_DragOver;
                chessBoardItem.DragLeave -= ChessBoardItem_DragLeave;
                chessBoardItem.Drop -= ChessBoardItem_Drop;
            }
        }

        private void ChessBoardItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is ChessBoardItem chessBoardItem)
            {
                e.Handled = true;
                try
                {
                    if (chessBoardItem.CanDrag)
                    {
                        var task = chessBoardItem.StartDragAsync(e.GetCurrentPoint(chessBoardItem));
                    }
                }
                catch (TaskCanceledException)
                {

                }
            }
        }

        private async void ChessBoardItem_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if (sender is ChessBoardItem chessBoardItem)
            {
                if (_draggedPiece != null)
                {
                    args.Cancel = true;
                    return;
                }

                args.AllowedOperations = DataPackageOperation.Move;

                Square square = (Square)chessBoardItem.Content;
                _draggedPiece = square.Piece;

                DragOperationDeferral deferral = args.GetDeferral();

                // Render the drag visual
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                double scale = 1.5;
                await renderTargetBitmap.RenderAsync(
                    chessBoardItem.DragElement,
                    (int)(chessBoardItem.DragElement.ActualWidth * scale),
                    (int)(chessBoardItem.DragElement.ActualHeight * scale)
                    );
                IBuffer buffer = await renderTargetBitmap.GetPixelsAsync();
                SoftwareBitmap bitmap = SoftwareBitmap.CreateCopyFromBuffer(
                    buffer,
                    BitmapPixelFormat.Bgra8,
                    renderTargetBitmap.PixelWidth,
                    renderTargetBitmap.PixelHeight
                    );
                args.DragUI.SetContentFromSoftwareBitmap(
                    bitmap,
                    new Point(renderTargetBitmap.PixelWidth / 2, renderTargetBitmap.PixelHeight / 2)
                    );

                square.Piece = null;
                chessBoardItem.CanDrag = false;

                deferral.Complete();
            }
        }

        private void ChessBoardItem_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            if (sender is ChessBoardItem chessBoardItem)
            {
                if (args.DropResult == DataPackageOperation.None)
                {
                    ((Square)chessBoardItem.Content).Piece = _draggedPiece;
                    chessBoardItem.CanDrag = true;
                }
                _draggedPiece = null;
            }
        }

        private void ChessBoardItem_DragEnter(object sender, DragEventArgs e)
        {
            if (_draggedPiece != null)
            {
                e.AcceptedOperation = DataPackageOperation.Move;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;

                VisualStateManager.GoToState((Control)sender, "DragTarget", false);

                e.Handled = true;
            }
        }

        private void ChessBoardItem_DragOver(object sender, DragEventArgs e)
        {
            if (_draggedPiece != null)
            {
                e.AcceptedOperation = DataPackageOperation.Move;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;

                VisualStateManager.GoToState((Control)sender, "DragTarget", false);

                e.Handled = true;
            }
        }

        private void ChessBoardItem_DragLeave(object sender, DragEventArgs e)
        {
            if (_draggedPiece != null)
            {
                e.AcceptedOperation = DataPackageOperation.None;
                e.DragUIOverride.IsCaptionVisible = false;
                e.DragUIOverride.IsGlyphVisible = false;

                VisualStateManager.GoToState((Control)sender, "NotDragging", false);

                e.Handled = true;
            }
        }

        private void ChessBoardItem_Drop(object sender, DragEventArgs e)
        {
            if (_draggedPiece != null && sender is ChessBoardItem chessBoardItem)
            {
                e.AcceptedOperation = DataPackageOperation.Move;
                ((Square)chessBoardItem.Content).Piece = _draggedPiece;
                chessBoardItem.CanDrag = true;

                VisualStateManager.GoToState((Control)sender, "NotDragging", false);

                e.Handled = true;
            }
        }

        private Piece _draggedPiece;
    }
}
