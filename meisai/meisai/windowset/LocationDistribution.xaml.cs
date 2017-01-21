using meisai.government;
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
using System.Windows.Shapes;

namespace meisai.windowset
{
    /// <summary>
    /// Interaction logic for LocationDistribution.xaml
    /// </summary>
    public partial class LocationDistribution : Window
    {
        Government goverment;

        public LocationDistribution(Government goverment_)
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 600;
            this.Top = 0;
            goverment = goverment_;
            InitializeComponent();
            refreshenable.Checked += Refreshenable_Checked;
        }

        private void Refreshenable_Checked(object sender, RoutedEventArgs e)
        {
            if (refreshenable.IsChecked.Value) dotMap.Refresh();
        }

        public void Refresh()
        {
            dotMap.positions = goverment.positions;
            dotMap.relationship = goverment.relationship;
            if (refreshenable.IsChecked.Value) dotMap.Refresh();
        }

        public bool CanBeClose = false;
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (CanBeClose)
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
