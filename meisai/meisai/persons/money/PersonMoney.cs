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
        public int money;
        static int producttendency;
        int consumetendency;//倾向与性别和人种有关
        public int taxrate = 0;
        public int welfare = 0;
        public int tax = 0;//交税
        //每一段时间调用以下函数，个人挣钱和花钱
        public void deltaTAfter(int day)
        {
            //挣钱 + 花钱
            //money += product()-tax;
            if (money > AllParameter.basicconsumtion)
                money -= AllParameter.basicconsumtion;//判定这人有没有钱
            else
            {
                welfare = -money + AllParameter.basicconsumtion;
                money = 0;
            }
            //没钱的话政府给福利
        }
        //生产
        public int product(PersonState state, PersonEducation education, int day = 365)
        {
            int product_;
            if (state.Age > AllParameter.graduateage)
            {
                product_ = state.funage() * education.EduLevel * AllParameter.productparameter * producttendency;
            }
            else product_ = 0;
            product_ *= (int)(Math.Sqrt(money));
            tax = product_ * taxrate;
            return product_;
        }
        //消费
        public int consumption(PersonState state, PersonEducation education)
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
                consumption_ = AllParameter.basicconsumtion + consumetendency * product(state, education) * (1 - taxrate);
            }
            return consumption_;
        }


        public int Money()
        {
            return money;
        }
    }
}
