using meisai.persons.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace meisai.Tools
{
    public static class AllParameter
    {
        /*
         * 所有的参数（除了注释掉的和以下列出的）都在SliderControl里面可以更改
         * 
         */
        //基础消费
        public static int basicconsumption = 5000;
        //孩子的单方基本消费
        public static int childbasicconsumption = 2500;
        //退休年龄
        public static int retireage = 60;
        //毕业年龄
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

        public static double tech_impro_rate = 0.2;
        #region 挣钱
        //失业最低工资
        public static int minimumwage = 10000;
        
        //工资——智商系数
        public static double IQproductparameter = 0.4;

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
            return 1;  
        }
        #endregion
        
            //对年龄的函数，使用在PersonMoney的生产中
        public static int productOfAge(int Age)
        {
            double poa;
            poa = 0.1 * Math.Pow(Age, 3.0) / (Math.Exp(0.0806 * Age) - 1);
            
            return (int)poa;          
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
        public static double gov_edu_rate = 0.5;
        #endregion

        #region 颜色显示
        public static Brush[] colorBrush = new Brush[10] { Brushes.Brown, Brushes.Red,
            Brushes.Orange, Brushes.Yellow, Brushes.Green, Brushes.Blue,
            Brushes.Purple, Brushes.Gray, Brushes.White, Brushes.Black};
        #endregion
        #region 年龄显示配置
        public static int MaxAge = 200;
        #endregion
        #region 婚姻生子
        public static double marriageRate = 0.05;
        public static int minMarriageAge = 20;
        public static int maxMarriageAge = 40;
        public static bool ifWillMarriage(double Lsquare)
        {
            //这个数可以改！！越小意味着随距离的关系越明显，但跑得会慢
            double ratio = 0.0001;
            return (ratio * RandomGen.getDouble()) / Lsquare > 1;
        }
        public static double Inheritance_tax_rate = 0.85;
        #endregion

        #region 初始年龄分布
        static double[] ageDistriAndWeight = new double[] {43.2024, 58.7072,
            79.0705, 105.555, 139.663, 183.156, 238.069, 
            306.708, 391.639, 495.663, 621.765, 773.047, 952.633, 1163.55,
            1408.58, 1690.13, 2010.01, 2369.28, 2768.04, 3205.31, 3678.79,
            4184.86, 4718.42, 5272.92, 5840.45, 6411.8, 6976.76, 7524.32,
            8043.04, 8521.44, 8948.39, 9313.58, 9607.89, 9823.79, 9955.65, 10000};
        public static int GetAge()
        {
            int MaxAge = ageDistriAndWeight.Length;
            double sum = 0;
            for (int i=0; i< MaxAge; i++)
            {
                sum += ageDistriAndWeight[i];
            }
            double target = RandomGen.getDouble() * sum;
            double targetSum = 0;
            int age = 0;
            for (int i=0; i<MaxAge; i++)
            {
                targetSum += ageDistriAndWeight[i];
                if (targetSum > target)
                {
                    age = i;
                    break;
                }
            }
            return age;
        }
        #endregion
        #region 幸福
        //个人幸福指数
        public static double happiness_index=1;
        //政府幸福指数
        public static double gov_happy_index = 1;
        //求和
        public static double sum(double[] num)
        {
            double sum_=0;
            foreach(double Num in num)
            {
                sum_ += Num;
            }
            return sum_;
        }
        //求方差
        public static double standard_deviation(double[] num)
        {
            double sd=0;
            double av=0;
            double powsum = 0;
            int count = 0;

            foreach(double Num in num)
            {
                av += Num;
                count++;
            }
            av/= count;
            foreach(double Num in num)
            {
                powsum += Num * Num;
            }
            sd = Math.Sqrt(-av * av + (powsum / count));
            return sd;
        }
        #endregion
    }

}
