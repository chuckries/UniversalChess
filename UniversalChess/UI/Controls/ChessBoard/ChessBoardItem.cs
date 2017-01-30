using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [TemplatePart(Name = ContentPresetner_PART, Type = typeof(ContentControl))]
    public sealed class ChessBoardItem : ContentControl
    {
        private ChessBoard ChessBoard { get { return (ChessBoard)ItemsControl.ItemsControlFromItemContainer(this); } }

        public FrameworkElement DragElement { get { return _contentPresener; } }

        public ChessBoardItem()
        {
            this.DefaultStyleKey = typeof(ChessBoardItem);
        }

        protected override void OnApplyTemplate()
        {
            _contentPresener = (ContentControl)GetTemplateChild(ContentPresetner_PART);

            base.OnApplyTemplate();
        }

        private const string ContentPresetner_PART = "ContentPresenter";
        private ContentControl _contentPresener;
    }
}
