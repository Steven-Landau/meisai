﻿using System;
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
        public Int64 allProduct = 0;
        public double GDPvarience;//国内生产总值和方差
      //  public int GDH;
        public double GDHvarience;//国内幸福总值和方差
        public long gov_edu_expen=0;//教育总支出
        public long gov_wel_expen=0;//福利总支出
        public long gov_tax=0;//政府总税收
        public Int64 allConsumption =0;
        //失业者
        public int jobless = 0;




    }
}
