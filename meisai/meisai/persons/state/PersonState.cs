﻿using meisai.Tools;
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
        //性别
        public Gender gender = Gender.NULL;
        //种族
        public Race race = Race.Lazy;
        //年龄
        public int Age = 25;
        //死亡率及状态（是否将要死亡）
        public double Deathrate = 0.01;
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
            Age++;//年龄增加
            //随年龄而变的死亡率
            Deathrate = AllParameter.basicdeathrate +
                AllParameter.age_deathrate *
                Math.Pow((Age - AllParameter.least_age_deathrate), 2);
        }

    }
    public enum Gender { NULL, Male, Female }
    public enum Race { Creative, Lazy }
    //技能状态，包括技能水平和类型等
    public class PersonEducation
    {
        //教育水平
        public double EduLevel = 1;
        public bool studying = false;

        public void getstudydeltaT(int day)
        {
            // isworking = false;//学习期间不能工作
            EduLevel++;
            //  money -= fee;//交学费

        }
    }

}