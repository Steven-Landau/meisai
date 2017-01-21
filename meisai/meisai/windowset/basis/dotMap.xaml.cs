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
    public partial class dotMap : UserControl
    {
        //地图界限
        public double Xmin = 0;
        public double Xmax = 1;
        public double Ymin = 0;
        public double Ymax = 1;
        //个人位置
        public Point[] position;
        public List<IntPair> relationship;

        public dotMap()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            //先画位置
            for (int i=0; i<position.Length; i++)
            {

            }
        }
    }
}
