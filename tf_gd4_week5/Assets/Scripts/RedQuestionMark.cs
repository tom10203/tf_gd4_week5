using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedQuestionMark : MonoBehaviour
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

    public bool instantiateSweepers = false;
    //public AudioSource spawnSound;
    public GameObject sweepers;

    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        island = GameObject.FindGameObjectWithTag("Island");
    }
    void Update()
    {
        timer += Time.deltaTime * floatSpeed;
        float sine = Mathf.Sin(timer);
        transform.Translate(Vector3.forward * floatHeight * sine * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        //if (instantiateSweepers)
        //{
        //    Instantiate(sweepers, island.transform);
        //    instantiateSweepers = false;
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            spawnManager.redQuestionMarkTimer = 0f;
            spawnManager.isRedQInScene = false;
            Instantiate(sweepers, island.transform);
            Destroy(gameObject);
        }
    }
}