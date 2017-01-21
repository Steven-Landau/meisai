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

namespace meisai.windowset.basis
{
    /// <summary>
    /// Interaction logic for histogram.xaml
    /// </summary>
    public partial class Histogram : UserControl
    {
        public Histogram()
        {
            InitializeComponent();
        }
        public void Refresh()
        {
            //可以用了
            //MessageBox.Show("Width=" + canvas.ActualWidth);
        }
    }
}
