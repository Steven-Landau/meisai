using meisai.persons.relation;
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
    /// Interaction logic for dotMap.xaml
    /// </summary>
    public partial class DotMap : UserControl
    {
        //地图界限
        public double Xmin = 0;
        public double Xmax = 1;
        public double Ymin = 0;
        public double Ymax = 1;
        //个人位置
        public Point[] positions = new Point[1] { new Point(0.5, 0.5)};
        public List<KeyValuePair<int, SingleRelation>> relationship = 
            new List<KeyValuePair<int, SingleRelation>>();

        public DotMap()
        {
            InitializeComponent();
            this.SizeChanged += DotMap_SizeChanged;
        }

        private void DotMap_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            //先删除所有
            canvas.Children.Clear();
            //先画位置
            double Xratio = canvas.ActualHeight / (Xmax - Xmin);
            double Yratio = canvas.ActualWidth / (Ymax - Ymin);
            for (int i=0; i<positions.Length; i++)
            {
                Ellipse dot = new Ellipse();
                dot.Width = 3;
                dot.Height = 3;
                dot.StrokeThickness = 0;
                dot.Fill = AllParameter.colorBrush[i % 10];
                canvas.Children.Add(dot);
                Canvas.SetBottom(dot, Xratio * (positions[i].X - Xmin));
                Canvas.SetLeft(dot, Yratio * (positions[i].Y - Ymin));
            }
            foreach (KeyValuePair<int, SingleRelation> x in relationship)
            {
                Line line = new Line();
                line.StrokeThickness = 1;
                line.Stroke = Brushes.Black;
            }
        }
    }
}
