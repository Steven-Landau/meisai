﻿using meisai.persons.career;
using meisai.persons.money;
using meisai.persons.relation;
using meisai.persons.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.persons
{
    /*
     * 这个类是个人，包括几个类：状态、金钱、事业、关系
     */
    public class Person
    {
        public int nowID = 0; //这个是当前状态下的个人编号，在government的personlist里
        public PersonState state = new PersonState();
        public PersonMoney money = new PersonMoney();
        public PersonCareer career = new PersonCareer();
        public PersonRelationShip relationShip = new PersonRelationShip();

        //这个是每一段时间以后调用一下，人可以挣钱，花钱，交换钱，交友，生子等等
        public void deltaTAfter(int day = 365)
        {
            state.deltaTAfter(day);
            money.deltaTAfter(state, day);
            state.Death(state.Deathrate(state.Age,money));
            if (state.IfWillDie)
            {
                //孩子变成孤儿，配偶变成单身狗
                foreach (SingleRelation sr in relationShip.relations)
                {
                    switch (sr.type)
                    {
                        case PersonRelationType.Child:
                            sr.targetPerson.relationShip.deleteRelationWith(this);
                            break;
                        case PersonRelationType.Father:
                            sr.targetPerson.relationShip.deleteRelationWith(this);
                            break;
                        case PersonRelationType.Husband:
                            sr.targetPerson.relationShip.deleteRelationWith(this);
                            break;
                        case PersonRelationType.Mother:
                            sr.targetPerson.relationShip.deleteRelationWith(this);
                            break;
                        case PersonRelationType.Wife:
                            sr.targetPerson.relationShip.deleteRelationWith(this);
                            break;
                    }
                }
            }
            for (int i=0; i<relationShip.relations.Count; i++)
            {
                if (relationShip.relations[i].type == PersonRelationType.Child &&
                    relationShip.relations[i].targetPerson.state.Age < 18)
                {
                    //是孩子！！！要付孩子的教育费
                }
            }
        }
        public int getMyMoney() => money.money;

        public int happiness()
        {
            int happiness_ = 0;

            return happiness_;
            //幸福 
        }


    }
}
