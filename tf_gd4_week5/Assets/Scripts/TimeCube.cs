using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCube : MonoBehaviour
{
    SpawnManager spawnManager;
    Material material;
    public float alphaTimer = 1f;
    public float buffer = 5f;
    public AudioSource spawnSound;
    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        material = GetComponent<MeshRenderer>().material;
        spawnSound.Play();
    }

    private void Update()
    {
        alphaTimer -= Time.deltaTime / buffer;
        if (alphaTimer <= 0)
        {
            spawnManager.timeCubeTimer = 0f;
            spawnManager.spawnTimeCube = true;
            Destroy(gameObject);
            
        }
        Color color = material.color;
        color.a = alphaTimer;
        material.color = color;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            print($"time cube collision");
            spawnManager.IncreaseBuffer();
            spawnManager.timeCubeTimer = 0f;
            spawnManager.spawnTimeCube = true;
            Destroy(gameObject);
        }
    }
}
