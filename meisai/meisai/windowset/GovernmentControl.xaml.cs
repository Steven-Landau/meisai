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
    /// Interaction logic for GovernmentControl.xaml
    /// </summary>
    public partial class GovernmentControl : Window
    {
        SliderControl[] sliders;
        SliderControlData[] sliderDatas;

        public GovernmentControl()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = 1200;
            this.Top = 0;
            InitializeComponent();
            sliders = new SliderControl[] { slider0, slider1, slider2, slider3,
                slider4, slider5, slider6, slider7, slider8, slider9};
            sliderDatas = new SliderControlData[] { new Slider0(), new Slider1(),
                new Slider2(), new Slider3()};
            for (int i=0; i<sliders.Length; i++)
            {
                if (i<sliderDatas.Length)
                {
                    sliders[i].sliderControlData = sliderDatas[i];
                }
                sliders[i].init();
            }
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
        class Slider0 : SliderControlData
        {
            int Max = 10000;
            public Slider0()
            {
                showName = "基础消费";
                InitLocation = AllParameter.basicconsumption / (double)Max;
            }
            public override String setValue(double ratio)
            {
                AllParameter.basicconsumption = (int)(ratio * Max);
                return "" + AllParameter.basicconsumption;
            }
        }
        class Slider1 : SliderControlData
        {
            int Max = 5000;
            public Slider1()
            {
                showName = "孩子的单方基本消费";
                InitLocation = AllParameter.childbasicconsumption / (double)Max;
            }
            public override String setValue(double ratio)
            {
                AllParameter.childbasicconsumption = (int)(ratio * Max);
                return "" + AllParameter.childbasicconsumption;
            }
        }
        class Slider2 : SliderControlData
        {
            int Max = 200;
            public Slider2()
            {
                showName = "退休年龄";
                InitLocation = AllParameter.retireage / (double)Max;
            }
            public override String setValue(double ratio)
            {
                AllParameter.retireage = (int)(ratio * Max);
                return "" + AllParameter.retireage;
            }
        }
        class Slider3 : SliderControlData
        {
            int Max = 200;
            public Slider3()
            {
                showName = "毕业年龄";
                InitLocation = AllParameter.graduateage / (double)Max;
            }
            public override String setValue(double ratio)
            {
                AllParameter.retireage = (int)(ratio * Max);
                return "" + AllParameter.retireage;
            }
        }
    }
}
