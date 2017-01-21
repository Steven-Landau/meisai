using meisai.persons.state;
using meisai.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace meisai.persons.money
{
    /*
     * 保存金钱相关的信息，还有挣钱花钱相关事宜
     */
    public class PersonMoney
    {
        //为简化起见，先存一个money，以后可以增加贷款信用等等
        public int money = AllParameter.init_money;
        //public double producttendency = 0;
        //public int welfare = 0;
        public int tax = 0;//交税
        public int productMoney = 0;
        public int welfareMoney = 0;

        //每一段时间调用以下函数，个人挣钱和花钱
        public void deltaTAfter(PersonState state, int day)
        {
            //挣钱 + 花钱
            productMoney = product(state);
            money += productMoney - tax + welfareMoney;
            money -= consumption(state);
        }
        //生产
        public int product(PersonState state, int day = 365)
        {
            int product_money;
            welfareMoney = 0;
            if (state.Age < AllParameter.graduateage || 
                state.Age > AllParameter.retireage)
            {
                product_money = 0;
                welfareMoney = AllParameter.basicconsumtion;
            }
            else
            {
                //先获得系数，即此系数*已有资金=新增资金
               // Console.WriteLine(state.IQ);
                double product_money_d =
                  (AllParameter.IQproductparameter*state.IQ)
                    * Math.Sqrt(money) *
                    AllParameter.productOfAge(state.Age) *
                    Math.Sqrt(state.education.EduLevel) *        
                    AllParameter.producttendency(state.race);
                //Console.WriteLine(product_money_d);
                product_money = (int)product_money_d;
               if (product_money < AllParameter.minimumwage)
                {
                    //失业了
                    product_money = 0;
                    welfareMoney = AllParameter.basicconsumtion;
                }
            }
            tax = (int)(product_money * AllParameter.taxRate());
           // Console.WriteLine(product_money);
            return product_money;
        }
        //消费
        public int consumption(PersonState state)
        {
            int consumption_;
            
            // Determine taxmode  ,  stupid
            AllParameter.TaxMode taxmode;
            if(productMoney < 10000)
                taxmode = AllParameter.TaxMode.Zero;
            else if(productMoney < 20000)
                taxmode = AllParameter.TaxMode.Low;
            else if(productMoney < 50000)
                taxmode = AllParameter.TaxMode.Medium;
            else if(productMoney < 80000)
                taxmode = AllParameter.TaxMode.High;
            else    
                taxmode = AllParameter.TaxMode.Extreme;

            // I am a pig I only write if else.....
            
            if (state.Age < AllParameter.graduateage || state.Age > AllParameter.retireage)
            {
                consumption_ = AllParameter.basicconsumtion;
            }
            else if (productMoney == 0)
            {
                consumption_ = AllParameter.basicconsumtion;
            }
            else
            {
                consumption_ = (int)(AllParameter.basicconsumtion +
                    AllParameter.consumetendency(state.race, state.gender) *
                    (productMoney - AllParameter.basicconsumtion) * (1 - AllParameter.taxRate(taxmode)));
            }
           // Console.WriteLine(consumption_);
            return consumption_;
        }


        public int Money()
        {
            return money;
        }
    }
}
