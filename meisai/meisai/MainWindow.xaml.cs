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
        #region 组件开关
        bool winGovernmentOnOff = true;
        bool ageDistributionOnOff = true;
        bool locationDistributionOnOff = true;
        #endregion
        Government government;
        WinGovernment winGovernment;
        AgeDistribution ageDistribution;
        LocationDistribution locationDistribution;
        int nowDay = 0;

        public MainWindow() 
        {
            InitializeComponent();
            RandomGen.Initiate();
            government = new Government();
            if (winGovernmentOnOff)
            {
                winGovernment = new WinGovernment(government);
                winGovernment.Show();
            }
            if (ageDistributionOnOff)
            {
                ageDistribution = new AgeDistribution(government);
                ageDistribution.Show();
            }
            if (locationDistributionOnOff)
            {
                locationDistribution = new LocationDistribution(government);
                locationDistribution.Show();
            }

            buttonstartayear.Click += Buttonstartayear_Click;
            MathematicaOut.Out("test", "A", new String[] { "a", "b" });
        }
        private void Refresh()
        {
            textBlockday.Text = "" + nowDay;
            textBlockyear.Text = "" + (nowDay / 365);

            if (winGovernmentOnOff) winGovernment.Refresh();
            if (ageDistributionOnOff) ageDistribution.Refresh();
            if (locationDistributionOnOff) locationDistribution.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (winGovernmentOnOff)
            {
                winGovernment.CanBeClose = true;
                winGovernment.Close();
            }
            if (ageDistributionOnOff)
            {
                ageDistribution.CanBeClose = true;
                ageDistribution.Close();
            }
            if (locationDistributionOnOff)
            {
                locationDistribution.CanBeClose = true;
                locationDistribution.Close();
            }
            base.OnClosing(e);
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
    }
}
