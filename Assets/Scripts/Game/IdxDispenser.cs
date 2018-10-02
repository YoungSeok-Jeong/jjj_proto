using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdxDispenser {

    private int m_curIdx;

    private static IdxDispenser s_instance = null;
    public static int get()
    {
        if (s_instance == null)
        {
            s_instance = new IdxDispenser();
        }

        return s_instance.pop();
    }

    private IdxDispenser()
    {
        m_curIdx = 1;
    }

    public int pop()
    {
        return m_curIdx++;
    }
}
