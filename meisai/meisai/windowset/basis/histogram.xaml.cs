using meisai.Tools;
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
        public int linesCount = 5; //显示几条基准线
        public double[] data = new double[1] { 1 };
        public double maxData = 1;

        public Histogram()
        {
            InitializeComponent();
            this.SizeChanged += Histogram_SizeChanged;
        }

        private void Histogram_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            //可以用了
            //MessageBox.Show("Width=" + canvas.ActualWidth);
            double height = canvas.ActualHeight - 20;
            double allWidth = canvas.ActualWidth;
            double width = allWidth / data.Length;
            canvas.Children.Clear(); //清除所有child
            if (height < 0) return;
            //绘制
            for (int i=0; i<data.Length; i++)
            {
                Rectangle rect = new Rectangle();
                Rectangle rect1 = new Rectangle();
                rect.Height = height * (data[i] / maxData);
                rect1.Height = 20;
                rect.Width = width;
                rect1.Width = width;
                rect.Fill = AllParameter.colorBrush[i % 10];
                rect1.Fill = AllParameter.colorBrush[i % 10];
                rect.StrokeThickness = 1;
                rect1.StrokeThickness = 0;
                rect.Stroke = Brushes.Black;
                canvas.Children.Add(rect);
                canvas.Children.Add(rect1);
                Canvas.SetBottom(rect, 20);
                Canvas.SetBottom(rect1, 0);
                Canvas.SetLeft(rect, width * i);
                Canvas.SetLeft(rect1, width * i);
            }
            double lineHeight = height / linesCount;
            for (int i=0; i <= linesCount; i++)
            {
                Line line = new Line();
                line.X1 = 0;
                line.X2 = allWidth;
                line.Y1 = line.Y2 = lineHeight * i;
                line.Stroke = AllParameter.colorBrush[i % 10];
                canvas.Children.Add(line);
            }
        }
    }
}
