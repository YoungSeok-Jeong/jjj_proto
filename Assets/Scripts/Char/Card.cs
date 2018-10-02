using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public enum CardAction : int
    {
        APPEAR
        , SELECTED
        , USED
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doAction(CardAction action)
    {
        //액션을 수행
        switch (action)
        {
            case CardAction.APPEAR:
                break;
                
            case CardAction.SELECTED:
                StartCoroutine(_selected());
                break;

            case CardAction.USED:
                StartCoroutine(_used());
                break;
        }
    }

    public void endAction()
    {
        //액션을 종료 시키고
        //GameMgr에 다음 플로우를 요청
    }

    //여기부턴 behavior로 빼던가 하자
    public IEnumerator _selected()
    {
        //스케일 up & down
        while (true)
        {
            yield return null;
            break;
        }

        endAction();
    }


    public IEnumerator _used()
    {
        //1. 확대
        //2. 이펙트와 함꼐 사라짐
        //3. 옵션에 따라 소멸 or 더미로 이동
        while (true)
        {
            yield return null;
            break;
        }

        endAction();
    }
}
