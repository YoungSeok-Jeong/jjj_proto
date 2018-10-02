using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_MoveMissile : Behavior_Move {

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
    }
}
