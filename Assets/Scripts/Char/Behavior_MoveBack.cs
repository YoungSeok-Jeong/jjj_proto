﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Behavior_MoveBack : Behavior_Move
{
    public override void prepareSubValue()
    {
        this.m_animationName = "move";
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

        m_actor.endAction(Actor.ActorAction.IDLE);
    }
}
