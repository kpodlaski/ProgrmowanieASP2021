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

namespace WindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int x = 34;
        int y = 35;
        bool isRunning = false;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                Thread t = new Thread(new ThreadStart(animation));
                t.Start();
                StartButton.Content = "Stop";
            }
            else
            {
                isRunning = false;
                StartButton.Content = "Start";
            }
            //Canvas.SetLeft(Ball, x); //Canvas.SetTop(Ball, y);
        }
        private void animation()
        {
            while (isRunning)
            {
                x += 1;
                y += 1;
                Dispatcher.BeginInvoke(
                    new Action(() => {
                        Ball.Margin = new Thickness(x, y, 0, 0);
                }));
                Thread.Sleep(30);
            }
        }
    }
}
