using meisai.persons.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.Tools
{
    public static class AllParameter
    {
        
        //基础消费
        public static int basicconsumtion = 5000;
        //退休年龄
        public static int retireage = 52;
        public static int graduateage = 18;
        ////基础死亡率，只要是个人就会有这么大概率死
        //public static double basicdeathrate = 0.008;
        ////死亡率随年龄的二次函数的系数
        //public static double age_deathrate = 0.01;
        ////死亡率最低的点
        //public static int least_age_deathrate = 9;

        #region 钱
        //初始的钱
        public static int init_money = 100000;
        #region 挣钱
        //失业最低工资
        public static int minimumwage = 10000;
        
        //工资——智商系数
        public static double IQproductparameter = 1.6;

        //倾向与性别和人种有关
        #region 生产倾向
        public static double producttendency(Race race)
        {
            switch (race)
            {
                case Race.Creative:
                    return 1.5;
                case Race.Lazy:
                    return 1;
            }
            return 0;
        }
        #endregion
        //对年龄的函数，使用在PersonMoney的生产中
        public static int productOfAge(int Age)
        {
            return -(Age - 35) * (Age - 35) + 289;
        }
       
        #endregion
        #region 消费倾向
        public static double consumetendency(Race race, Gender gender)
        {

            double t1 = 1, t2 = 1;
            switch (race)
            {
                case Race.Lazy:
                    t1 = 0.8*(RandomGen.getDouble()/2 + 0.5);
                    break;
                case Race.Creative:
                    t1 = 0.7*(RandomGen.getDouble()/3 + 0.67);
                    break;
                default:
                    t1 = 1;
                    break;
            }
            switch (gender)
            {
                case Gender.Female:
                    t2 = 1.2;
                    break;
                case Gender.Male:
                    t2 = 1;
                    break;
                default:
                    t2 = 1;
                    break;
            }

            return t1 * t2;
        }
        #endregion
        #region 税率
        public enum TaxMode { Zero,Low, Medium, High, Extreme };
        public static TaxMode taxMode;
       public static double taxRate(TaxMode taxMode)
        {
            switch (taxMode)
            {
                case TaxMode.Zero:
                    return 0;
                case TaxMode.Low:
                    return 0.05;
                case TaxMode.Medium:
                    return  0.15;
                case TaxMode.High:
                    return 0.3;
                case TaxMode.Extreme:
                    return 0.5;
            }
            return 0;
        }
        #endregion
        #endregion
        #region education
        //基础学费
        public static int bassic_edu_fee = 1000;
        //政府承担学费比例
        public static int gov_edu_rate = 1;
        #endregion

    }
}
