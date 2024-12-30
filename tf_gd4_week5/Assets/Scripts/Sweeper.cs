using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    public bool isLeft;
    public float moveSpeed = 1f;
    public float fadeInSpeed = 1f;
    Material material;
    float timer = 0f;
    int sign;
    float deadZone;
    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        sign = isLeft ? 1 : -1;
        deadZone = transform.position.x * -1f;
      

    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            transform.Translate(Vector3.right * sign * moveSpeed * Time.deltaTime);
        }
        else
        {
            Color color = new Color();
            color = material.color;
            color.a = timer;
            material.color = color;
        }

        if (isLeft)
        {
            if (transform.position.x >= deadZone)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x <= deadZone)
            {
                Destroy(gameObject);
            }
        }
        

    }
}
