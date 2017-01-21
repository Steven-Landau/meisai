using meisai.government;
using meisai.windowset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.ComponentModel;
using meisai.Tools;

namespace meisai 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Government government;
        WinGovernment winGovernment;
        AgeDistribution ageDistribution;
        int nowDay = 0;

        public MainWindow() 
        {
            InitializeComponent();
            RandomGen.Initiate();
            government = new Government();
            winGovernment = new WinGovernment(government);
            ageDistribution = new AgeDistribution(government);
            winGovernment.Show();
            ageDistribution.Show();

            buttonstartayear.Click += Buttonstartayear_Click;
        }
        private void Refresh()
        {
            textBlockday.Text = "" + nowDay;
            textBlockyear.Text = "" + (nowDay / 365);

            winGovernment.Refresh();
            ageDistribution.Refresh();
        }

        private void deltaTAfter(int day = 365)
        {
            nowDay += day;
            government.deltaTAfter(day);


            Refresh();
        }

        private void Buttonstartayear_Click(object sender, RoutedEventArgs e)
        {
            deltaTAfter(365);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            winGovernment.CanBeClose = true;
            winGovernment.Close();
            base.OnClosing(e);
        }
    }
}
