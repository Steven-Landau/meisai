using meisai.government.state;
using meisai.persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.government
{
    public class Government
    {
        static List<Person> personList = new List<Person>();
        GovernmentState state = new GovernmentState();

        public Government()
        {
            state.govMoney = 1000*10000;
            for (int i = 0; i < 10000; i++)
            {
                personList.Add(new Person());
            }
        }
        public void deltaTAfter(int day = 365)
        {
            //先进行政策范围内的事情：
            //1,开展教育

            //再遍历每个人实现个人的改变，包括赚钱等等
           //每个人挣钱
            foreach (Person person in personList)
            {
                person.deltaTAfter(day);
            }
            //政府收税      
            foreach (Person person in personList)
            {
                state.govMoney += person.money.tax;
                state.govMoney -= person.money.welfareMoney;

            }
            //死人
            for (int i = 0; i < personList.Count; i++)
            {
                //死人
                if (personList[i].state.IfWillDie)
                {
                    //政府收回死人的钱
                    state.govMoney += personList[i].money.money;
                    personList.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            
            //统计新的状态
            sumUpStates();
        }
        private void sumUpStates()
        {
            //统计散落在人间的金钱总和
            state.allMoney = 0;
            state.allConsumption = 0;
            state.allProduct = 0;
            state.jobless = 0;
            foreach (Person person in personList)
            {
                state.allMoney += person.getMyMoney();
                state.allConsumption += person.money.consumption(person.state);
                state.allProduct += person.money.product(person.state);
                if (person.money.product(person.state) == 0) state.jobless++;
            }
        }
        public Int64 GetGovMoney() => state.govMoney;
        public Int64 GetAllMoney() => state.allMoney;
        public Int64 GetAllConsumption() => state.allConsumption;
        public Int64 GetAllProduct() => state.allProduct;
        public int GetMenCount() => personList.Count;
        public int GetJobless() => state.jobless;
    }
}
