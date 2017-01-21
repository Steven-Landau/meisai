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
                    t1 = 1;
                    break;
                case Race.Creative:
                    t1 = 0.9;
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
        public enum TaxMode { Low};
        public static TaxMode taxMode;
        public static double taxRate()
        {
            switch (taxMode)
            {
                case TaxMode.Low:
                    return 0.1;
            }
            return 0;
        }
        #endregion
        #endregion
    }
}
