using meisai.government;
using meisai.Tools;
using meisai.windowset.basis;
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
    /// Interaction logic for AgeDistribution.xaml
    /// </summary>
    public partial class AgeDistribution : Window
    {
        Government goverment;

        public AgeDistribution(Government goverment_)
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 0;
            this.Top = 0;
            goverment = goverment_;
            InitializeComponent();
            //this.AddChild(histogram);
        }
        public void Refresh()
        {
            histogram.maxData = 1;
            histogram.data = goverment.ageDistrib;
            histogram.Refresh();

            //输出到mathematica
            MathematicaOut.Out("ageDistribution", "age", goverment.ageDistrib);
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
