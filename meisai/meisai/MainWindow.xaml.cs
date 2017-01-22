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
        Government government = null;
        WinGovernment winGovernment = null;
        AgeDistribution ageDistribution = null;
        LocationDistribution locationDistribution = null;
        GovernmentControl governmentControl;
        int nowDay = 0;

        public MainWindow() 
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 300;
            InitializeComponent();
            InitWindowSet();
            governmentControl = new GovernmentControl();
            governmentControl.Show();
            Refresh();
            buttonstartayear.Click += Buttonstartayear_Click;
            buttonend.Click += Buttonend_Click;
            //MathematicaOut.Out("test", "A", new String[] { "a", "b" });
        }

        private void Buttonend_Click(object sender, RoutedEventArgs e)
        {
            nowDay = 0;
            CloseWindowSet();
            InitWindowSet();
            Refresh();
            MessageBox.Show("重新开始了！");
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
            CloseWindowSet();
            governmentControl.CanBeClose = true;
            governmentControl.Close();
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

        private void InitWindowSet()
        {
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
        }

        private void CloseWindowSet()
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
        }
    }
}
