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
        public List<SingleRelation> relations = new List<SingleRelation>();
        public void deleteRelationWith(Person person)
        {
            for (int i=0; i<relations.Count; i++)
            {
                if (relations[i].targetPerson == person)
                {
                    relations.RemoveAt(i);
                    i--;
                }
            }
        }
        public Person findRelation(PersonRelationType type)
        {
            foreach (SingleRelation x in relations)
            {
                if (x.type == type) return x.targetPerson;
            }
            return null;
        }
    }
    public enum PersonRelationType { NULL, Father, Mother, Child, Wife, Husband};
    public class SingleRelation
    {
        public PersonRelationType type = PersonRelationType.NULL;
        //关系对象s
        public Person targetPerson = null;
        public SingleRelation(PersonRelationType type_, Person targetPerson_)
        {
            type = type_;
            targetPerson = targetPerson_;
        }
    }
}
