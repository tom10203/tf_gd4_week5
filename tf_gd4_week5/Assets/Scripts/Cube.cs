using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    SpawnManager spawnManager;
    Material material;
    public float alphaTimer = 1f;
    public float buffer;
    public bool isMultiCube = false;
    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        material = GetComponent<MeshRenderer>().material;
        print(spawnManager);
    }

    private void Update()
    {
        alphaTimer -= Time.deltaTime / buffer;
        if (alphaTimer <= 0)
        {
            if (!isMultiCube)
            {
                spawnManager.HandleCubeOutOfTime(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        Color color = material.color;
        color.a = alphaTimer;
        material.color = color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            spawnManager.UpdateScore();
            spawnManager.RemoveCube(gameObject, 0f);    
        }
    }

}
