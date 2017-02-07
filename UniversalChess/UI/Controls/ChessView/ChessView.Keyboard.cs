using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace UniversalChess.UI.Controls
{
    public partial class ChessView
    {
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            FocusNavigationDirection direction;
            switch (e.Key)
            {
                case VirtualKey.Up:     direction = FocusNavigationDirection.Up; break;
                case VirtualKey.Down:   direction = FocusNavigationDirection.Down; break;
                case VirtualKey.Left:   direction = FocusNavigationDirection.Left; break;
                case VirtualKey.Right:  direction = FocusNavigationDirection.Right; break;
                default: base.OnKeyDown(e); return;
            }

            FocusManager.TryMoveFocus(direction);
            e.Handled = true;
        }
    }
}
