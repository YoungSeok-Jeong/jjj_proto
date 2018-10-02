using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour {
    private static GameMgr s_instance = null;

    private float m_timer = 0.0f;
    private float m_attackTimer = 0.0f;
    private List<Actor> m_actors = new List<Actor>();

    private Actor m_attacker;
    private Actor m_defender;

    private Vector3 m_attackerPos = new Vector3(-5.0f, 0.15f, -2.0f);
    private Vector3 m_defenderPos = new Vector3(5.0f, 0.15f, -2.0f);

    private Config_Game m_config;

    public static GameMgr get()
    {
        return s_instance;
    }

    void Awake()
    {
        s_instance = this;
    }

	// Use this for initialization
	void Start () {
        //load settings
        GameObject Settings = GameObject.Find("Settings");
        Config_Game config = Settings.GetComponent<Config_Game>();
        m_config = config;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_timer == 0.0f)
        {
            prepare();
        }
        m_timer += Time.deltaTime;
        m_attackTimer += Time.deltaTime;

        if (m_attackTimer > 4.0f)
        {
            m_attackTimer = 0;
            int idx = m_attacker.decideTarIdx();
            Vector3 pos = getTargetPos(idx);
            m_attacker.setTarPos(pos);
            m_attacker.startAttack();
        }
	}

    private void prepare()
    {
        //load plane and dummy
        //config 값대로 각도 조절
        GameObject plane = GameObject.Find("Plane");
        GameObject planeDummy = GameObject.Find("PlaneDummy");
        //plane.transform.rotation = Quaternion.Euler(new Vector3(-m_config.Angle, 0, 0));
        //planeDummy.transform.rotation = Quaternion.Euler(new Vector3(-m_config.Angle, 0, 0));

        //load prefab
        GameObject obj = Resources.Load("Prefab/111") as GameObject;

        //attaacker 생성
        GameObject attacker = Instantiate(obj, planeDummy.transform);

        //config값을 참고하여 각도 및 위치 조정
        //attacker.transform.localRotation = Quaternion.Euler(new Vector3(m_config.Angle, 0, 0));
        attacker.transform.localPosition = m_config.AttackerPos;
        //실제 attacker를 제어하는 스크립트 m_attacker에 세팅
        m_attacker = attacker.GetComponent<Actor>();
        m_attacker.setAttacker(true);
        m_actors.Add(m_attacker);

        //attacker와 생성 과정 동일
        GameObject defender = Instantiate(obj, planeDummy.transform);
        //defender.transform.localRotation = Quaternion.Euler(new Vector3(m_config.Angle, 0, 0));
        defender.transform.localPosition = m_config.DefenderPos;
        defender.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        m_defender = defender.GetComponent<Actor>();
        m_defender.setAttacker(false);
        m_actors.Add(m_defender);
    }

    public Actor getActorByIdx(int idx)
    {
        foreach (Actor _actor in m_actors)
        {
            if (_actor.getidx() == idx)
            {
                return _actor;
            }
        }

        return null;
    }

    public Vector3 getTargetPos(int idx)
    {
        Actor target = getActorByIdx(idx);
        if (target != null)
        {
            return target.transform.localPosition;
        }

        return Vector3.zero;
    }
}
