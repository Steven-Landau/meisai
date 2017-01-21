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
        public double[] data = new double[1] { 1 };
        private Brush[] brush = new Brush[10] { Brushes.Brown, Brushes.Red,
            Brushes.Orange, Brushes.Yellow, Brushes.Green, Brushes.Blue,
            Brushes.Purple, Brushes.Gray, Brushes.White, Brushes.Black};
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
                rect.Fill = brush[i % 10];
                rect1.Fill = brush[i % 10];
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
        }
    }
}
