using meisai.government.state;
using meisai.persons;
using meisai.persons.state;
using meisai.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace meisai.government
{
    public class Government
    {
        static List<Person> personList = new List<Person>();
        GovernmentState state = new GovernmentState();
        //年龄分布，从0岁到99岁
        public double[] ageDistrib = new double[100];

        public Government()
        {//初态
            state.govMoney = 1000*10000;
            for (int i = 0; i < 10000; i++)
            {
                personList.Add(new Person());
            }
            /*//平均年龄分布
            for (int i=0;i<10; i++)
            {
                for(int j=0;j<1000;j++)
                {
                    personList[1000 * i + j].state.Age = 10*i+(j+50)/100;
                }
            }*/
            //线性年龄分布
            for(int i=0;i<10000;i++)
            {
                personList[i].state.gender = (Gender)(i / 5000);
                personList[i].state.race = (Race)(i / 5000);
                personList[i].state.Age = 18 + (36 * (i-5000)) / 10000;
            }
        }
        public void deltaTAfter(int day = 365)
        {
            //置零
            state.gov_edu_expen = 0;//教育总支出
            state.gov_wel_expen = 0;//福利总支出
            state.gov_tax = 0;
            //先进行政策范围内的事情：
            //1,开展全民教育
            
            foreach (Person person in personList)
            {
           
                if (person.state.Age < 18)        
                state.gov_edu_expen+= (int)(AllParameter.bassic_edu_fee *
                        AllParameter.gov_edu_rate *
                        Math.Sqrt(person.state.education.EduLevel));
              
            }
            state.govMoney -= state.gov_edu_expen;
            //再遍历每个人实现个人的改变，包括赚钱等等
            //每个人挣钱
            foreach (Person person in personList)
            {
                person.deltaTAfter(day);
            }
            //政府收税      
            foreach (Person person in personList)
            {
                state.gov_tax += person.money.taxMoney;
                state.gov_wel_expen += person.money.welfareMoney;

            }
            state.govMoney += state.gov_tax;
            state.govMoney -= state.gov_wel_expen;
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
            getAgeAttribution();
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
        private void getAgeAttribution()
        {
            for (int i = 0; i < ageDistrib.Length; i++) ageDistrib[i] = 0;
            int yichu = 0;
            foreach (Person person in personList)
            {
                if (person.state.Age < ageDistrib.Length && person.state.Age > 0)
                {
                    ageDistrib[person.state.Age] ++;
                }
                else
                {
                    yichu ++;
                }
            }
            for (int i = 0; i < ageDistrib.Length; i++) ageDistrib[i] /= 
                    personList.Count;
            if (yichu > 0)
            {
                MessageBox.Show("由于有" + yichu + "人年龄不小于" + ageDistrib.Length + 
                    "岁，导致年龄分布没有显示它们");
            }
        }
        public long GetGovMoney() => state.govMoney;
        public long GetAllMoney() => state.allMoney;
        public long GetAllConsumption() => state.allConsumption;
        public long GetAllProduct() => state.allProduct;
        public int GetMenCount() => personList.Count;
        public int GetJobless() => state.jobless;
        public long Gettax() => state.gov_tax;
        public long Getedu() => state.gov_edu_expen;
        public long Getwel() => state.gov_wel_expen;
    }
}
