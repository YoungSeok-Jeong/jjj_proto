using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Damaged : Behavior {

    public override void prepareSubValue()
    {
        this.m_animationName = "hitted";
        this.m_loop = false;
    }

    public override IEnumerator doAction()
    {
        while (!m_spineAnimationState.GetCurrent(0).IsComplete)
        {
            yield return null;
        }
        m_actor.endAction(Actor.ActorAction.IDLE);
    }
}
