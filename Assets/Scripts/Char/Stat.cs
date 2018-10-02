using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour {

    public int basePow;
    public int baseHp;
    public int baseDef;
    public int baseDex;

    public int pow;
    public int hp;
    public int def;
    public int dex;

    public int curHp;

	void Start()
    {
        basePow = 100;
        baseHp = 100;
        baseDef = 100;
        baseDex = 100;

        pow = 100;
        hp = 100;
        def = 100;
        dex = 100;

        curHp = hp;
    }

    public int getDamage(int pow, Stat defender)
    {
        return 100;
    }
}
