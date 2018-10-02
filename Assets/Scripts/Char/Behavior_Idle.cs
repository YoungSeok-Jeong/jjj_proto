using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Idle : Behavior {

    public override void prepareSubValue()
    {
        this.m_animationName = "idle";
        this.m_loop = true;
    }

    public override IEnumerator doAction()
    {
        yield return null;
    }
}
