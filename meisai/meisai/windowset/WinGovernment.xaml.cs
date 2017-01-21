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
using System.ComponentModel;

namespace meisai.windowset
{
    /// <summary>
    /// Interaction logic for WinGovernment.xaml
    /// </summary>
    public partial class WinGovernment : Window
    {
        Government goverment;

        public WinGovernment(Government goverment_)
        {
            goverment = goverment_;
            InitializeComponent();
            
        }
        public void Refresh()
        {
            textBlockgovmoney.Text = "" + goverment.GetGovMoney();
            textBlockallmoney.Text = "" + goverment.GetAllMoney();
            textBlockmen.Text = "" + goverment.GetMenCount();
            textBlockavermoney.Text = "" + (goverment.GetAllMoney()/ 
                (double)goverment.GetMenCount());
            textBlockaverproduct.Text = "" + goverment.GetAllProduct()/ (double)goverment.GetMenCount();
            textBlockaverconsumption.Text = "" + goverment.GetAllConsumption()/ (double)goverment.GetMenCount();
            joblessrate.Text=""+goverment.GetJobless()/ (double)goverment.GetMenCount();
        }

        public bool CanBeClose = false;
        protected override void OnClosing(CancelEventArgs e)
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
