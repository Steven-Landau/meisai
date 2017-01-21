using meisai.persons.money;
using meisai.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace meisai.persons.state
{
    /*
     * 保存所有的信息
     */
    public class PersonState
    {
        //智商
        public double IQ = 0.5 + RandomGen.getDouble()/2; 
        //性别
        public Gender gender = Gender.Male;
        //种族
        public Race race = Race.Lazy;
        //年龄
        public int Age = 5;
        //死亡率及状态（是否将要死亡）
        public double Deathrate(int Age, PersonMoney pm) {
            if(Age <= 1)
                return 0.008* (1 - pm.Money()/1000000.0);
            else if(Age <= 4)
                return 0.00029* (1 - pm.Money()/1000000.0);
            else if(Age <= 14)
                return 0.00015* (1 - pm.Money()/1000000.0);
            else if(Age <= 24)
                return 0.0008* (1 - pm.Money()/1000000.0);
            else if(Age <= 34)
                return 0.00104* (1 - pm.Money()/1000000.0);
            else if(Age <= 44)
                return 0.00184* (1 - pm.Money()/1000000.0);
            else if(Age <= 54)
                return 0.0042* (1 - pm.Money()/1000000.0);
            else if(Age <= 64)
                return 0.00877* (1 - pm.Money()/1000000.0);
            else if(Age <= 74)
                return 0.02011* (1 - pm.Money()/1000000.0);
            else if(Age <= 84)
                return 0.05011;
            else
                return 0.1296;
        }
        public bool IfWillDie = false;
        //教育程度
        public PersonEducation education = new PersonEducation();


        public void Die()
        {
            IfWillDie = true;
            //人的财产交给政府
            //孩子变成孤儿
            //这个人消失
        }
        public void Death(double Deathrate_)
        {
            double RandKey = RandomGen.getDouble();
            //Console.WriteLine(RandKey);
            if (RandKey < Deathrate_) Die();
        }
        public void deltaTAfter(int day = 365)
        {
            Age++;
            //年龄增加
        }

    }
    public enum Gender {Male, Female }
    public enum Race { Creative, Lazy }
    //技能状态，包括技能水平和类型等
    public class PersonEducation
    {
        //教育水平
        public double EduLevel = 1;
        public bool studying = false;

        public void getstudydeltaT(int day)
        {
            
            EduLevel++;
        }
    }

}
