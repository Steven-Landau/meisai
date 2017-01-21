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
    /// Interaction logic for SliderControl.xaml
    /// </summary>
    public partial class SliderControl : UserControl
    {
        public SliderControlData sliderControlData = new SliderControlDemo();
        public SliderControl()
        {
            InitializeComponent();
            slider.ValueChanged += Slider_ValueChanged;
        }
        public void init()
        {
            textBlockname.Text = sliderControlData.showName;
            slider.Value = sliderControlData.InitLocation * 10;
            Slider_ValueChanged(null, null);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Console.WriteLine("slider:" + slider.Value);
            textBlockvalue.Text = 
                sliderControlData.setValue(slider.Value / 10);
        }
        class SliderControlDemo : SliderControlData
        {
            public SliderControlDemo() { showName = "未定义"; }
            public override String setValue(double ratio)
            {
                return "" + ratio;
            }
        }
    }
}
