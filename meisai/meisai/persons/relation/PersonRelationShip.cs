using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.persons.relation
{
    /*
     * 保存人际关系，包括婚姻，友谊，师生，老板和员工等等
     */
    public class PersonRelationShip
    {
        List<SingleRelation> relations = new List<SingleRelation>();
    }
    public enum PersonRelationType { NULL, Friend, Couple };
    public class SingleRelation
    {
        PersonRelationType type = PersonRelationType.NULL;
        //关系对象s
        Person targetPerson = null;
    }
}
