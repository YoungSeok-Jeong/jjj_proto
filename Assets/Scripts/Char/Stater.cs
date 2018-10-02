using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater : MonoBehaviour {

    private int m_state = 0;
    private int m_frame = 0;
    private int m_nextState = 0;
    private float m_timer = 0.0f;
    private float m_stateTimer = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (this.m_nextState != 0)
        {
            this.m_state = this.m_nextState;
            this.m_frame = 0;
            this.m_stateTimer = 0;
            this.m_nextState = 0;
        }
        
        this.m_timer += Time.deltaTime;
        this.m_stateTimer += Time.deltaTime;
        this.m_frame++;
	}

    public void changeState(int state, bool forced = false)
    {
        if (forced)
        {
            this.m_nextState = state;
        }
        else
        {
            if (this.m_state != state)
            {
                this.m_nextState = state;
            }
        }
    }

    public int getState()
    {
        return this.m_state;
    }

    public bool isFirstFrame()
    {
        return this.m_frame == 1;
    }

    public float getStateTimer()
    {
        return this.m_stateTimer;
    }
}
