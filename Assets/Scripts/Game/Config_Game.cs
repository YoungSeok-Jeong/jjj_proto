using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config_Game : MonoBehaviour {

    public int Angle = 60;
    public Vector3 AttackerPos = new Vector3(-5.5f, 0.15f, -2.0f);
    public Vector3 DefenderPos = new Vector3(5.5f, 0.15f, -2.0f);

    public Vector3 getAttackPos(bool targetIsAttacker)
    {
        if (targetIsAttacker)
        {
            return AttackerPos + new Vector3(2.5f, 0, 0);
        }
        else
        {
            return DefenderPos + new Vector3(-2.5f, 0, 0);
        }
    }
}
