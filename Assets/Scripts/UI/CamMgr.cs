using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMgr : MonoBehaviour {

    private Vector3 m_startPos;
    private bool m_touching = false;
    private float m_distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.m_startPos = touch.position;
                    this.m_touching = true;
                    break;

                case TouchPhase.Moved:
                    if (Vector3.Distance(this.m_startPos, touch.position) > 2.0f && this.m_touching)
                    {
                        float dx = this.m_startPos.x - touch.position.x;
                        float dy = this.m_startPos.y - touch.position.y;
                        this.transform.Translate(dx * Time.deltaTime * 0.5f, dy * Time.deltaTime * 0.5f, 0.0f);
                    }

                    this.m_startPos = touch.position;
                    
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    this.m_touching = false;
                    break;
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                this.m_touching = true;
                this.m_distance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                this.m_touching = false;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float distance = Vector2.Distance(touch1.position, touch2.position);
                if (distance > 2.0f && this.m_touching)
                {
                    this.transform.Translate(0.0f, 0.0f, (distance - this.m_distance) * Time.deltaTime * 0.2f);
                }
                this.m_distance = distance;
                
            }           
        }

        if (Input.touchCount == 3)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            Touch touch3 = Input.GetTouch(2);
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began || touch3.phase == TouchPhase.Began)
            {
                this.m_startPos = touch1.position;
                this.m_touching = true;
            }
            else if (touch1.phase == TouchPhase.Canceled || touch2.phase == TouchPhase.Canceled || touch3.phase == TouchPhase.Canceled
            || touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended || touch3.phase == TouchPhase.Ended)
            {
                this.m_touching = false;
            }
            else if (touch1.phase == TouchPhase.Moved && this.m_touching)
            {
                float dx = touch1.position.x - this.m_startPos.x;
                if (Mathf.Abs(dx) > 2.0f)
                {
                    Vector3 move = new Vector3(dx, 0.0f, 0.0f);
                    this.transform.RotateAround(Vector3.zero, Vector3.up, dx * 3.0f * Time.deltaTime);
                }
                this.m_startPos = touch1.position;
            }
            
        }

        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.m_startPos = Input.mousePosition;
                this.m_touching = true;
            }
            else if (Input.GetMouseButton(0))
            {
                if (Mathf.Abs(Input.mousePosition.x - this.m_startPos.x) > 2.0f)
                {
                    this.transform.RotateAround(Vector3.zero, Vector3.up, (Input.mousePosition.x - this.m_startPos.x) * 10.0f * Time.deltaTime);
                }
                
                this.m_startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.m_touching = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                this.m_startPos = Input.mousePosition;
                this.m_touching = true;
            }
            else if (Input.GetMouseButton(1))
            {
                if (Vector3.Distance(this.m_startPos, Input.mousePosition) > 2.0f)
                {
                    this.transform.Translate((this.m_startPos - Input.mousePosition) * Time.deltaTime);
                }
                
                this.m_startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                this.m_touching = false;
            }
        }
	}
}
