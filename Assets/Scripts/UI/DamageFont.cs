using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFont : MonoBehaviour {

    public Vector3 m_tarPos;
    public float m_speed;

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(action());
	}
	

	// Update is called once per frame
    void Update()
    {
    }
       

    public IEnumerator action()
    {
        while (true)
        {
            Vector3 newPos = Vector3.MoveTowards(this.transform.localPosition, m_tarPos, m_speed * Time.deltaTime);
            this.transform.localPosition = newPos;
            if (newPos.Equals(m_tarPos))
            {
                break;
            }
            else
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);

        TextMesh mesh = GetComponent<TextMesh>();
        float alpha = mesh.color.a;
        while (mesh.color.a > 0)
        {
            alpha -= (Time.deltaTime);
            if (alpha <= 0.0f)
            {
                alpha = 0.0f;
            }
            mesh.color = new Color(mesh.color.r, mesh.color.g, mesh.color.b, alpha);
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
