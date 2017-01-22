using meisai.government.state;
using meisai.persons;
using meisai.persons.relation;
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
        static List<Person> personList;
        GovernmentState state;
        //年龄分布，从0岁到99岁
        public double[] ageDistrib;
        public Point[] positions;
        public List<IntPair> relationship;

        public Government()
        {//初态
            personList = new List<Person>();
            ageDistrib = new double[AllParameter.MaxAge];
            state = new GovernmentState();
            state.govMoney = 1000*10000;
            for (int i = 0; i < 10000; i++)
            {
                personList.Add(new Person());
            }        
            //平均年龄分布
            for(int i=0;i<10000;i++)
            {
                personList[i].state.gender = (Gender)(i / 5000);
                personList[i].state.race = (Race)(i / 5000);
                //personList[i].state.Age = 18 + (36 * (i-5000)) / 10000;
                personList[i].state.Age = AllParameter.GetAge();
            }
            refreshStates();
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
                state.gov_wel_maternal_expen += person.money.welfareMaternalLeave;
            }
            state.govMoney += state.gov_tax;
            state.govMoney -= state.gov_wel_expen;
            state.govMoney -= state.gov_wel_maternal_expen;
            //政府发给孩子的救助
            state.govChildrenFee = 0;
            foreach (Person person in personList)
            {
                state.govChildrenFee += person.childNeedGovFee;
            }
            state.govMoney -= state.govChildrenFee;
            //死人
            for (int i = 0; i < personList.Count; i++)
            {
                //死人
                if (personList[i].state.IfWillDie)
                {
                    //政府收回死人的钱
                    state.govMoney += (long)AllParameter.Inheritance_tax_rate*
                        personList[i].money.money;
                    personList.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            updatepositions();
            //婚恋生子
            marriage();

            //统计新的状态
            refreshStates();
        }
        private void refreshStates()
        {
            //统计新的状态
            sumUpStates();
            getAgeAttribution();
            updatepositions();
            updateID();
            updateRelationship();
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
                
                if (person.state.isjobless) state.jobless++;
            }
        }
        private void getAgeAttribution()
        {
            for (int i = 0; i < ageDistrib.Length; i++) ageDistrib[i] = 0;
            int yichu = 0;
            foreach (Person person in personList)
            {
                if (person.state.Age < ageDistrib.Length && person.state.Age >= 0)
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
        private void updatepositions()
        {
            positions = new Point[personList.Count];
            for (int i=0; i< personList.Count; i++)
            {
                positions[i] = personList[i].state.position;
            }
        }
        private void updateID()
        {
            for (int i=0; i<personList.Count; i++)
            {
                personList[i].nowID = i;
            }
        }
        private void updateRelationship()
        {
            relationship = new List<IntPair>();
            for (int i = 0; i < personList.Count; i++)
            {
                foreach (SingleRelation x in personList[i].relationShip.relations)
                {
                    relationship.Add(new IntPair(i, x.targetPerson.nowID));
                }
            }
        }
        private void marriage()
        {
            //先找到所有的可婚的人
            List<Person> maleList = new List<Person>();
            List<Person> femaleList = new List<Person>();
            foreach (Person person in personList)
            {
                //每个孩子都消费2倍的basicconsumption
                if (person.money.money > AllParameter.childbasicconsumption && 
                    person.state.Age > AllParameter.minMarriageAge && 
                    person.state.Age < AllParameter.maxMarriageAge &&
                    person.state.maternalLeave == 0)
                {
                    //可以付钱供养孩子
                    switch (person.state.gender)
                    {
                        case Gender.Male:
                            maleList.Add(person);
                            break;
                        case Gender.Female:
                            femaleList.Add(person);
                            break;
                    }
                }
            }
            //已经找到了所有的可以生孩子的人的列表
            int maxMarry = (int)(personList.Count * AllParameter.marriageRate);
            int count = 0;
            while (count < maxMarry && maleList.Count != 0 && femaleList.Count != 0)
            {
                //先随机找一个男的，男的找女的
                int maleIndex = (int)(RandomGen.getDouble() * maleList.Count);
                Person maleM = maleList[maleIndex % maleList.Count];
                maleList.Remove(maleM); //删除掉这个男性
                Person femaleM;
                do
                {
                    femaleM = femaleList[((int)(RandomGen.getDouble()
                        * femaleList.Count)) % femaleList.Count];
                } while (AllParameter.ifWillMarriage(femaleM.state.position.X *
                    femaleM.state.position.X + femaleM.state.position.Y *
                    femaleM.state.position.Y));
                //找到了一个合适的女人
                femaleList.Remove(femaleM); //删除掉这个女性
                //第一年就花费
                maleM.money.money -= AllParameter.childbasicconsumption;
                femaleM.money.money -= AllParameter.childbasicconsumption;
                //新建小孩儿，并添加到列表中
                Person child = new Person();
                personList.Add(child);
                //设置孩子的位置
                child.state.position.X = (maleM.state.position.X + 
                    femaleM.state.position.X) / 2;
                child.state.position.Y = (maleM.state.position.Y +
                    femaleM.state.position.Y) / 2;
                child.state.gender = (RandomGen.getDouble() > 0.5) ? 
                    Gender.Female : Gender.Male;
                child.state.IQ = maleM.state.IQ + femaleM.state.IQ;
                child.state.race = maleM.state.race;
                child.state.education.EduLevel = maleM.state.education.EduLevel +
                    femaleM.state.education.EduLevel;
                //产假
                maleM.state.maternalLeave = 0;
                femaleM.state.maternalLeave = 0;
                //添加三个人之间的关系
                child.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Father, maleM));
                child.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Mother, femaleM));
                maleM.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Child, child));
                maleM.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Wife, femaleM));
                femaleM.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Child, child));
                femaleM.relationShip.relations.Add(new SingleRelation(
                    PersonRelationType.Husband, maleM));
                //生了一个孩子，+1
                count++;
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
        public long GetwelMaternal() => state.gov_wel_maternal_expen;
        public double Getgov_happiness() => state.gov_happiness;

        public void SaveOnYear()
        {
            MathematicaOut.AppendYearData(MathematicaOut.WriteToList(new String[] {
                t(MainWindow.nowDay/365), t(state.allMoney), t(state.govMoney),
                t(state.allProduct), t(state.GDPvarience), t(state.GDHvarience),
                t(state.gov_edu_expen), t(state.gov_wel_expen),
                t(state.gov_wel_maternal_expen), t(state.gov_tax),
                t(state.allConsumption), t(state.jobless), t(state.govChildrenFee),
                MathematicaOut.WriteToList(tt(ageDistrib))}));
        }
        public String t<T>(T x) => x.ToString();
        public String[] tt(double[] a)
        {
            String[] s = new String[a.Length];
            for (int i = 0; i < s.Length; i++) s[i] = a[i].ToString();
            return s;
        }
        //这里是所有输出的名字列表，可以在mathematica里通过key_<名字>来代表下标
        //比如数组是A，则调用年份用A[key_year]来调用
        public static String[] savedName = new String[] {
            "year", "allMoney", "govMoney", "allProduct", "GDPvarience",
            "GDHvarience", "goveduexpen", "govwelexpen", "govwelmaternalexpen",
            "govtax", "allConsumption", "jobless", "govChildrenFee",
            "ageDistrib"};
    }
}
