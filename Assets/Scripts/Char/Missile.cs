using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Actor {

	// Use this for initialization
	void Start () {
        m_moveBehavior = this.gameObject.AddComponent<Behavior_MoveMissile>();
        m_moveBehavior.setSpeed(m_moveSpeed);
        m_moveBehavior.setTarPos(transformTarPos(m_tarPos));
        this.action(ActorAction.MOVE);
	}
	
	// Update is called once per frame
	void Update () {
        m_aliveTime += Time.deltaTime;
	}

    public override Vector3 transformTarPos(Vector3 pos)
    {
        return pos + new Vector3(0.0f, 1.5f, 0.0f);
    }

    void OnCollisionEnter(Collision coll)
    {
        Actor actor = coll.collider.GetComponent<Actor>();
        if (this.m_tarIdx == actor.getidx())
        {
            actor.action(Actor.ActorAction.DAMAGED);
            m_parent.finishAttack();
            
            // damage font
            GameObject prefab = Resources.Load<GameObject>("Prefab/Font/damage");
            GameObject damageText = Instantiate(prefab, actor.transform);
            TextMesh text = damageText.GetComponent<TextMesh>();
            text.text = "-254";

            GameObject prefab2 = Resources.Load<GameObject>("Prefab/Effect/effect_damaged");
            GameObject damageEffect = Instantiate(prefab2, actor.transform);
            //damageEffect.transform.localPosition = coll.transform.localPosition;


            Debug.Log("collision missile!! " + actor.getidx());
            Destroy(this.gameObject);
        }
    }
}
