using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.government.state
{
    class GovernmentState
    {
        //个人拥有的资产总和
        public long allMoney = 0;
        //政府所拥有的资产
        public long govMoney = 0;
        public int GDP;
        public double GDPvarience;//国内生产总值和方差
        public int GDH;
        public double GDHvarience;//国内幸福总值和方差
        public Int64 allProduct=0;
        public Int64 allConsumption =0;
        public int jobless = 0;



    }
}
