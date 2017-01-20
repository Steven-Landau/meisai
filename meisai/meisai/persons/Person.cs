using meisai.persons.career;
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
    class Person
    {
        public PersonState state = new PersonState();
        public PersonMoney money = new PersonMoney();
        public PersonCareer career = new PersonCareer();
        public PersonRelationShip relationShip = new PersonRelationShip();

        //这个是每一段时间以后调用一下，人可以挣钱，花钱，交换钱，交友，生子等等
        public void deltaTAfter(int day = 365)
        {
            state.Death(state.Deathrate);
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
