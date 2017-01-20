using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UniversalChess.UI.Controls
{
    public class IdToRankFileConverter : IValueConverter
    {
        private static string[] RANKS = { "8", "7", "6", "5", "4", "3", "2", "1" };
        private static string[] FILES = { "A", "B", "C", "D", "E", "F", "G", "H" };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int id = (int)value;

            if (parameter as string == "rank")
            {

                return RANKS[id / 8];

            }
            else
            {
                return FILES[id % 8];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
