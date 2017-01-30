using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.UI.Xaml.Data;

namespace SampleApp
{
    public class OrientationRadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (PieceColor)value == PieceColor.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? PieceColor.Black : PieceColor.White;
        }
    }
}
