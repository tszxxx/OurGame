using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client myClient;
        public MainWindow()
        {
            InitializeComponent();
            myClient = new Client();
            Thread MainThread = new Thread(new ThreadStart(Client.Begin));
            MainThread.Start();
        }

        private void onClick(object sender, RoutedEventArgs e)
        {

            myClient.IPAddr = myTextBox.Text.ToString();
        }
    }
}
