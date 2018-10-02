using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public abstract class Behavior : MonoBehaviour {

    protected SkeletonAnimation m_skeletonAnimation;

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    protected Spine.AnimationState m_spineAnimationState;
    protected Spine.Skeleton m_skeleton;
    protected Actor m_actor;
    protected string m_animationName;
    protected bool m_loop;

    void Start()
    {
        prepare();
        prepareSubValue();
    }

    public void action()
    {
        if (m_skeletonAnimation != null)
        {
            m_spineAnimationState.SetAnimation(0, m_animationName, m_loop);
        }
        
        StartCoroutine(doAction());
    }

    public void prepare()
    {
        m_skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (m_skeletonAnimation != null)
        {
            m_spineAnimationState = m_skeletonAnimation.AnimationState;
            m_skeleton = m_skeletonAnimation.Skeleton;
        }
        
        m_actor = GetComponent<Actor>();
    }

    public abstract void prepareSubValue();
    public abstract IEnumerator doAction();
}
