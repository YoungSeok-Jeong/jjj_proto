using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Attack : Behavior {

    protected float m_fireTime;
    protected bool isCollisionComplete;

    public void setFireTime(float fireTime)
    {
        m_fireTime = fireTime;
    }

    public override void prepareSubValue()
    {
        this.m_animationName = "atk";
        this.m_loop = false;
    }

    public override IEnumerator doAction()
    {
        float timer = 0.0f;
        bool doFire = false;
        isCollisionComplete = false;

        while (!m_spineAnimationState.GetCurrent(0).IsComplete || !isCollisionComplete)
        {
            timer += Time.deltaTime;
            if (!doFire && timer >= m_fireTime)
            {
                fire();
                doFire = true;
            }
            yield return null;
        }
        m_actor.endAction(Actor.ActorAction.MOVE_BACK);
    }

    public void setCollisionComplete(bool complete)
    {
        isCollisionComplete = complete;
    }

    public virtual void fire()
    {
        //나중에는 이 부분을 테이블로부터 세팅하자
        GameObject planeDummy = GameObject.Find("PlaneDummy");
        GameObject obj = Resources.Load("Prefab/Missile/dummy") as GameObject;
        GameObject missile = Instantiate(obj, planeDummy.transform);
        missile.transform.localPosition = this.transform.localPosition;

        Missile missileActor = missile.GetComponent<Missile>();
        Actor parent = GetComponent<Actor>();
        missileActor.initWithParent(parent);
        //missileActor.setMoveSpeed(10.0f);
    }
}
