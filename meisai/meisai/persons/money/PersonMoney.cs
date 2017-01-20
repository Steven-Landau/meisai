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
        public double producttendency = 0;
        int consumetendency = 1;//倾向与性别和人种有关
        public int taxrate = 0;
        public int welfare = 0;
        public int tax = 0;//交税
        //每一段时间调用以下函数，个人挣钱和花钱
        public void deltaTAfter(PersonState state, int day)
        {
            //挣钱 + 花钱
            money += product(state);// - tax;

            if (money > AllParameter.basicconsumtion)
                money -= consumption(state);//判定这人有没有钱
            else
            {
                welfare = -money + AllParameter.basicconsumtion;
                money = 0;
            }
            //没钱的话政府给福利
        }
        //生产
        public int product(PersonState state, int day = 365)
        {
            int product_money;
            if (state.Age > AllParameter.graduateage)
            {
                //先获得系数，即此系数*已有资金=新增资金
                double product_money_d = Math.Sqrt(money) *
                    AllParameter.productOfAge(state.Age) *
                    Math.Sqrt(state.education.EduLevel) *
                    AllParameter.productparameter *
                    AllParameter.producttendency(state.race);
                //Console.WriteLine(product_money_d);
                product_money = (int)product_money_d;
            }
            else product_money = 0;
            tax = (int)(product_money * AllParameter.taxRate());
            
            return product_money;
        }
        //消费
        public int consumption(PersonState state)
        {
            int consumption_;
            if (state.Age < AllParameter.graduateage)
            {
                consumption_ = AllParameter.basicconsumtion;
            }
            else if (state.Age > AllParameter.retireage)
            {
                consumption_ = AllParameter.basicconsumtion;
            }
            else
            {
                consumption_ = AllParameter.basicconsumtion + consumetendency * 
                    (product(state) - AllParameter.basicconsumtion) * (1 - taxrate);
            }
            //Console.WriteLine(consumption_);
            return consumption_;
        }


        public int Money()
        {
            return money;
        }
    }
}
