using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepersParent : MonoBehaviour
{

    private void Start()
    {
        transform.position = new Vector3(0f, 7f, 0f);
       
    }
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
