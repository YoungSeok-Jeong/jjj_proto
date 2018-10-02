using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_AttackFire : Behavior_Attack {

    public override void prepareSubValue()
    {
        this.m_animationName = "atk";
        this.m_loop = false;
    }

    public override void fire()
    {
        //나중에는 이 부분을 테이블로부터 세팅하자
        GameObject planeDummy = GameObject.Find("PlaneDummy");
        GameObject obj = Resources.Load("Prefab/Missile/fireball") as GameObject;
        GameObject missile = Instantiate(obj, planeDummy.transform);
        Missile missileActor = missile.GetComponent<Missile>();

        missile.transform.localPosition = missileActor.transformTarPos(this.transform.localPosition);
        Actor parent = GetComponent<Actor>();
        missileActor.initWithParent(parent);
    }
}
