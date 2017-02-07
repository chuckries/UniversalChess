using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniversalChess.UI.Controls
{
    [TemplatePart(Name = PiecePresenter_PART, Type = typeof(ContentPresenter))]
    public partial class ChessViewItem : Control
    {
        public int Id { get; private set; } = 0;

        public ChessViewItem()
        {
            DefaultStyleKey = typeof(ChessViewItem);
        }

        public ChessViewItem(int id) : this()
        {
            Id = id;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _piecePresenter = (ContentPresenter)GetTemplateChild(PiecePresenter_PART);
        }

        private const string PiecePresenter_PART = "PiecePresenter";

        private ContentPresenter _piecePresenter;
    }
}
