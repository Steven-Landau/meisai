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
            state.govMoney = 1000;
            for (int i = 0; i < 10000; i++)
            {
                personList.Add(new Person());
            }
        }
        public void deltaTAfter(int day = 365)
        {
            //先进行政策范围内的事情：
            //再遍历每个人实现个人的改变，包括赚钱等等
            foreach (Person person in personList)
            {
                person.deltaTAfter(day);
            }
            for (int i = 0; i < personList.Count; i++)
            {
                if (personList[i].state.IfWillDie)
                {
                    personList.RemoveAt(i);
                    i--;
                }
            }
            //统计新的状态
            sumUpStates();
        }
        private void sumUpStates()
        {
            //统计散落在人间的金钱总和
            state.allMoney = 0;
            foreach (Person person in personList)
            {
                state.allMoney += person.getMyMoney();
            }
        }
        public int GetGovMoney() => state.govMoney;
        public int GetAllMoney() => state.allMoney;
        public int GetMenCount() => personList.Count;
    }
}
