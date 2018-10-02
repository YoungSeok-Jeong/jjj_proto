using UnityEngine;

[ExecuteInEditMode]
public class ConstantScaleTransform : MonoBehaviour
{
    public Vector3 ConstantScale;

    private Transform myParent;

    private Vector3 oldScale;

    void Awake()
    {
        ConstantScale = transform.localScale;
    }

    void OnEnable()
    {
        myParent = transform.parent;
        oldScale = ConstantScale;     
    }    

    void Update ()
    {
        Vector3 tempScale;

        tempScale = transform.lossyScale;

        if (oldScale != tempScale)
        {
            Vector3 oldPosition = transform.localPosition;
            Vector3 oldRotation = transform.localEulerAngles;
            oldScale = tempScale;

            myParent.DetachChildren();

            transform.localScale = ConstantScale;

            transform.parent = myParent;

            transform.localPosition = oldPosition;
            transform.localEulerAngles = oldRotation;
        } 

    }
}
