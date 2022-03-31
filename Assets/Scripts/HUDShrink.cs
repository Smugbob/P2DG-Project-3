using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDShrink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("returnSize", 0.1f, 0.5f);
        Invoke("resize", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resize()
    {
        CancelInvoke("returnSize");
        transform.localScale = new Vector3(0.5f, 0.5f, 0);
        transform.position -= new Vector3(220, 0, 0);
    }

    void returnSize()
    {

        transform.localScale = new Vector3(1, 1, 1);
        Invoke("upSize", 0.2f);
    }

    void upSize()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0);
    }
}
