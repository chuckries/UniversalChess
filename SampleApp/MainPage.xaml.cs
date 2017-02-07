using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalChess.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Tuple<string, Color>> ColorList;
        public MainPage()
        {
            this.InitializeComponent();

            ColorList = new List<Tuple<string, Color>>();
            foreach (var color in  typeof(Colors).GetRuntimeProperties())
            {
                ColorList.Add(new Tuple<string, Color>(color.Name, (Color)color.GetValue(null)));
            }
        }

        private void TheChessView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int i = 0;
        }

        private void TheChessView_Loaded(object sender, RoutedEventArgs e)
        {
            TheChessView.Position = Position.RuyLopez;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string tag = (string)button.Tag;

            Position position = null;
            switch (tag)
            {
                case "Empty": position = Position.Empty; break;
                case "Starting": position = Position.Starting; break;
                case "RuyLopez": position = Position.RuyLopez; break;
            }

            TheChessView.Position = position;
        }
    }
}
