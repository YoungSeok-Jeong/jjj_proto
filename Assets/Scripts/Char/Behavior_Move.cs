using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Behavior_Move : Behavior
{
    protected Vector3 m_tarPos;
    protected float m_speed;

    public void setTarPos(Vector3 pos)
    {
        Debug.Log("set tar pos " + pos.ToString());
        m_tarPos = pos;
    }

    public void setSpeed(float speed)
    {
        m_speed = speed;
    }

    public override void prepareSubValue()
    {
        this.m_animationName = "run";
        this.m_loop = true;
    }

    public override IEnumerator doAction()
    {
        while (true)
        {
            Vector3 newPos = Vector3.MoveTowards(this.transform.localPosition, m_tarPos, m_speed * Time.deltaTime);
            this.transform.localPosition = newPos;
            if (newPos.Equals(m_tarPos))
            {
                break;
            }
            else
            {
                yield return null;
            }
        }

        m_actor.endAction(Actor.ActorAction.ATTACK);
    }
}
