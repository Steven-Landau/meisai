using meisai.persons.career;
using meisai.persons.money;
using meisai.persons.relation;
using meisai.persons.state;
using meisai.Tools;
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
        public int childNeedGovFee = 0;

        //这个是每一段时间以后调用一下，人可以挣钱，花钱，交换钱，交友，生子等等
        public void deltaTAfter(int day = 365)
        {
            childNeedGovFee = 0;
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
                //部分遗产继承给大儿子！！！
                Person child = relationShip.findRelation(PersonRelationType.Child);
                if (child != null)
                {
                    child.money.money +=(int)((1-AllParameter.Inheritance_tax_rate) *money.money);                   
                }
            }
            if (state.Age < AllParameter.graduateage && state.education.studying)
            {
                //需要向父母索要生活费和学费
                Person father = relationShip.findRelation(PersonRelationType.Father);
                Person mother = relationShip.findRelation(PersonRelationType.Mother);
                if (father != null && mother != null)
                {
                    //先要生活费，如果没有，则解除关系，另一方付全部学费，如果不行，则辍学
                    bool canFatherConsumption = father.tryGetBasisConsumption();
                    bool canMotherConsumption = mother.tryGetBasisConsumption();
                    if (!canFatherConsumption)
                    {
                        father.relationShip.deleteRelationWith(this);
                        relationShip.deleteRelationWith(father);
                        father = null;
                        childNeedGovFee += AllParameter.childbasicconsumption;
                    }
                    if (!canMotherConsumption)
                    {
                        mother.relationShip.deleteRelationWith(this);
                        relationShip.deleteRelationWith(mother);
                        mother = null;
                        childNeedGovFee += AllParameter.childbasicconsumption;
                    }
                    //再索要学费，这时候没有付生活费的已经解除关系了
                    int homeEducationFee = (int)((1 - AllParameter.gov_edu_rate) *
                        AllParameter.bassic_edu_fee *
                        Math.Sqrt(state.education.EduLevel));
                    if (father != null && mother != null)
                    {
                        //学费一人一半
                        bool faTuition = father.tryGetTuition(homeEducationFee / 2);
                        bool maTuition = mother.tryGetTuition(homeEducationFee / 2);
                        if (!faTuition && !maTuition)
                        {
                            //直接辍学
                            state.education.offStudy();
                        }
                        else if (faTuition && !maTuition)
                        {
                            bool faTuition2 =
                                father.tryGetTuition(homeEducationFee / 2);
                            if (!faTuition2) state.education.offStudy();
                        }
                        else if (!faTuition && maTuition)
                        {
                            bool maTuition2 =
                                mother.tryGetTuition(homeEducationFee / 2);
                            if (!maTuition2) state.education.offStudy();
                        }
                    }
                    else if (father == null && mother == null)
                    {
                        //直接辍学
                        state.education.offStudy();
                    }
                    else if (father != null)
                    {
                        //爸爸付全部
                        bool Tuition = father.tryGetTuition(homeEducationFee);
                        if (!Tuition) state.education.offStudy();
                    }
                    else if (mother != null)
                    {
                        //妈妈付全部
                        bool Tuition = mother.tryGetTuition(homeEducationFee);
                        if (!Tuition) state.education.offStudy();
                    }
                }
                else if (father != null)
                {
                    bool canFatherConsumption = father.tryGetBasisConsumption();
                    if (!canFatherConsumption)
                    {
                        father.relationShip.deleteRelationWith(this);
                        relationShip.deleteRelationWith(father);
                        father = null;
                        childNeedGovFee += AllParameter.childbasicconsumption;
                    }
                    //再索要学费，这时候没有付生活费的已经解除关系了
                    int homeEducationFee = (int)((1 - AllParameter.gov_edu_rate) *
                        AllParameter.bassic_edu_fee *
                        Math.Sqrt(state.education.EduLevel));
                    if (father != null)
                    {
                        bool faTuition = father.tryGetTuition(homeEducationFee);
                        if (!faTuition) state.education.offStudy();
                    }
                    else
                    {
                        //直接辍学
                        state.education.offStudy();
                    }
                }
                else if (mother != null)
                {
                    bool canMotherConsumption = mother.tryGetBasisConsumption();
                    if (!canMotherConsumption)
                    {
                        mother.relationShip.deleteRelationWith(this);
                        relationShip.deleteRelationWith(mother);
                        mother = null;
                        childNeedGovFee += AllParameter.childbasicconsumption;
                    }
                    //再索要学费，这时候没有付生活费的已经解除关系了
                    int homeEducationFee = (int)((1 - AllParameter.gov_edu_rate) *
                        AllParameter.bassic_edu_fee *
                        Math.Sqrt(state.education.EduLevel));
                    if (mother != null)
                    {
                        bool maTuition = mother.tryGetTuition(homeEducationFee);
                        if (!maTuition) state.education.offStudy();
                    }
                    else
                    {
                        //直接辍学
                        state.education.offStudy();
                    }
                }
                else
                {
                    childNeedGovFee += 2 * AllParameter.childbasicconsumption;
                }
            }
            //开始学习！！！！！
            state.education.getstudydeltaT(day);
        }
        public int getMyMoney() => money.money;

        public int happiness()
        {
            int happiness_ = 0;

            return happiness_;
            //幸福 
        }

        public bool tryGetBasisConsumption()
        {
            if (AllParameter.childbasicconsumption < money.money)
            {
                money.money -= AllParameter.childbasicconsumption;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool tryGetTuition(int tuition)
        {
            if (tuition < money.money)
            {
                //负的起
                money.money -= tuition;
                return true;
            }
            return false;
        }

    }
}
