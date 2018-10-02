using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;

public class Actor : MonoBehaviour
{

    #region Actor State Members
    private Stater m_stater;
    public enum ActorAction : int
    {
        IDLE
        , MOVE
        , MOVE_BACK
        , ATTACK
        , DAMAGED
    }
    #endregion

    #region Actor Behavior Members
    protected Behavior_Idle m_idleBehavior;
    protected Behavior_Move m_moveBehavior;
    protected Behavior_Move m_moveBackBehavior;
    protected Behavior_Attack m_attackBehavior;
    protected Behavior_Damaged m_damagedBehavior;
    
    #endregion

    #region Actor Attack Type
    public enum AttackType : int { NORMAL, FIRE }

    [Header("Essential Values")]
    public AttackType m_attackType = AttackType.NORMAL;
    public float m_moveSpeed = 10.0f;
    public float m_moveBackSpeed = 30.0f;
    public float m_fireTime = 1.0f;
    #endregion

    protected int m_idx;
    protected int m_tarIdx;
    protected Vector3 m_tarPos;
    protected Actor m_parent;

    protected bool m_isAttacker = true;
    protected float m_aliveTime = 0.0f;
    private SpineController m_spineController;


    // Use this for initialization
	void Start () {
        m_idx = IdxDispenser.get();
        m_stater = GetComponent<Stater>();
        m_spineController = GetComponent<SpineController>();

        m_idleBehavior = this.gameObject.AddComponent<Behavior_Idle>();
        m_moveBehavior = this.gameObject.AddComponent<Behavior_Move>();
        m_moveBehavior.setSpeed(m_moveSpeed);
        m_moveBehavior.setTarPos(Vector3.zero);
        m_moveBackBehavior = this.gameObject.AddComponent<Behavior_MoveBack>();
        m_moveBackBehavior.setSpeed(m_moveBackSpeed);
        m_damagedBehavior = this.gameObject.AddComponent<Behavior_Damaged>();

        switch (m_attackType)
        {
            case AttackType.NORMAL:
                m_attackBehavior = this.gameObject.AddComponent<Behavior_Attack>();
                break;
            case AttackType.FIRE:
                m_attackBehavior = this.gameObject.AddComponent<Behavior_AttackFire>();
                break;
        }

        m_attackBehavior.setFireTime(m_fireTime);

        if (!m_isAttacker)
        {
            m_spineController.ChangeAttachment("111_weapon_01", "111_weapon_01", "Char/Weapon/weapon_02", true);
            SkeletonAnimation ani = GetComponent<SkeletonAnimation>();
            //ani.Skeleton.flipX = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        m_aliveTime += Time.deltaTime;
	}

    public int getidx()
    {
        return m_idx;
    }

    public void setAttacker(bool isAttacker)
    {
        m_isAttacker = isAttacker;
    }

    public bool isAttacker()
    {
        return m_isAttacker;
    }

    public void action(ActorAction type)
    {
        switch (type)
        {
            case ActorAction.IDLE:
                Debug.Log("Idle action!");
                m_idleBehavior.action();
                break;
            case ActorAction.MOVE:
                Debug.Log("Move action!");
                m_moveBehavior.action();
                break;
            case ActorAction.MOVE_BACK:
                Debug.Log("MoveBack action!");
                m_moveBackBehavior.action();
                break;
            case ActorAction.ATTACK:
                Debug.Log("Attack action!");
                m_attackBehavior.action();
                break;
            case ActorAction.DAMAGED:
                Debug.Log("Damaged action!");
                m_damagedBehavior.action();
                break;
        }
    }

    public void endAction(ActorAction next)
    {
        action(next);
    }

    public void setTarPos(Vector3 pos)
    {
        m_tarPos = pos;

        if (m_moveBehavior != null)
        {
            m_moveBehavior.setTarPos(transformTarPos(pos));
        }
        if (m_moveBackBehavior != null)
        {
            m_moveBackBehavior.setTarPos(this.transform.localPosition);
        }
    }

    public Vector3 getTarPos()
    {
        return m_tarPos;
    }

    public int decideTarIdx()
    {
        m_tarIdx = m_isAttacker ? 2 : 1;
        return m_tarIdx;
    }

    public int getTarIdx()
    {
        return m_tarIdx;
    }

    public void setMoveSpeed(float speed)
    {
        m_moveSpeed = speed;
        if (m_moveBehavior != null)
        {
            m_moveBehavior.setSpeed(speed);
        }
    }

    public void setMoveBackSpeed(float speed)
    {
        m_moveBackSpeed = speed;
        if (m_moveBackBehavior != null)
        {
            m_moveBackBehavior.setSpeed(speed);
        }
    }

    public float getSpeed()
    {
        return m_moveSpeed;
    }

    public void startAttack()
    {
        decideTarIdx();
        if (m_attackType == AttackType.NORMAL)
        {
            this.action(ActorAction.MOVE);
        }
        else
        {
            this.action(ActorAction.ATTACK);
        }
    }

    public virtual Vector3 transformTarPos(Vector3 pos)
    {
        if (m_isAttacker)
        {
            return pos + new Vector3(-2.5f, 0.0f, 0.0f);
        }
        else
        {
            return pos + new Vector3(2.5f, 0.0f, 0.0f);
        }
    }

    public void finishAttack()
    {
        m_attackBehavior.setCollisionComplete(true);
    }

    public void initWithParent(Actor parent)
    {
        m_parent = parent;
        m_tarIdx = parent.getTarIdx();
        m_isAttacker = parent.isAttacker();
        setTarPos(parent.getTarPos());
    }
}
