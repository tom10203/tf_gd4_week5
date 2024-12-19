using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{

    public bool isHit = false;
    public float floatHeight;
    public float floatSpeed;
    public float rotationSpeed;
    float timer = 0f;
    SpawnManager spawnManager;

    public bool useIslandTransformPoint = false;
    public Vector3 position;
    public GameObject island;

    public AudioSource spawnSound;
    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        spawnSound.Play();
    }
    void Update()
    {
        timer += Time.deltaTime * floatSpeed;
        float sine = Mathf.Sin(timer);
        transform.Translate(Vector3.forward * floatHeight * sine * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        //if (useIslandTransformPoint)
        //{
        //    transform.position = island.transform.TransformPoint(position);
        //}
        //else
        //{
        //    transform.position = position;
        //}


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            spawnManager.multiCube = true;
            int random = Random.Range(0, 2) == 0 ? 1 : -1;
            if (random == 1)
            {
                spawnManager.multiBall = true;
            }
            else
            {
                spawnManager.multiCube = true;
            }
            spawnManager.isPowerUpInScene = false;
            spawnManager.powerUpTimer = 0f;
            Destroy(gameObject);
        }
    }
}
