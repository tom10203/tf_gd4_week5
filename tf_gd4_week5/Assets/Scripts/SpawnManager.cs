using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public Material multiCubeMaterial;
   
    public AudioSource ballSpawnAudio;
    public AudioSource cubeSpawnSound;
    public float destroyObjectBelowY;

    List<GameObject> balls = new List<GameObject>();
    List<GameObject> cubes = new List<GameObject>();

    public int noOfMultiBalls;
    int currentMultiBallSpawnBall = 0;
    int currentMultiBallSpawnCube = 0;
    int currentBalls = 0;
    int currentCubes = 0;
    float ballTimer;
    float cubeTimer;

    public bool multiBall = false;
    public bool multiCube = false;
    public float circleRadius = 11.2f;

    public float timeInBetweenSpawnBall;
    public float timeInBetweenSpawnCube;

    public GameObject cubePrefab;
    public GameObject island;
    public GameObject questionMarkPrefab;
    public GameObject timeCubePrefab;

    UIManager manager;

    public float timeInBetweenPowerupSpawn = 1f;
    public float powerUpTimer = 0f;
    public bool isPowerUpInScene = false;

    float gamePlayTimer = 0f;
    float cubeBuffer = 10;
    public float roundTime = 7f;

    float timeInBetweenTimeCubeSpawn = 3f;
    public float timeCubeTimer = 0f;
    public bool spawnTimeCube = true;

    void Start()
    {
        ballTimer = timeInBetweenSpawnBall;
        cubeTimer = timeInBetweenSpawnCube;
        SpawnBall(1, spawnPoint.position);
        SpawnCube(1, false, cubeBuffer);
        manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        gamePlayTimer += Time.deltaTime;
        timeCubeTimer += Time.deltaTime;

        if (gamePlayTimer > roundTime)
        {
            print($"round increase, cube buffer {cubeBuffer}");
            cubeBuffer -= 1;
            timeInBetweenTimeCubeSpawn++;
            cubeBuffer = Mathf.Clamp(cubeBuffer, 5f, 10f);
            timeInBetweenTimeCubeSpawn = Mathf.Clamp(timeInBetweenTimeCubeSpawn, 3f, 10f);
            gamePlayTimer = 0f;
        }

        if (timeCubeTimer > timeInBetweenTimeCubeSpawn && spawnTimeCube)
        {
            Instantiate(timeCubePrefab, GenerateRandomSpawnPoint(), spawnPoint.rotation, island.transform);
            spawnTimeCube = false;
        }

        CheckBallsYPos();
        CheckCubesYPos();
        HandlePowerUps();


        if (multiBall)
        {
            ballTimer += Time.deltaTime;
            if (currentMultiBallSpawnBall >= noOfMultiBalls)
            {
                currentMultiBallSpawnBall = 0;
                multiBall = false;
            }
            else
            {
                if (ballTimer >= timeInBetweenSpawnBall)
                {
                    SpawnBall(1, GenerateRandomSpawnPoint());
                    currentMultiBallSpawnBall++;
                    ballTimer = 0f;
                }
            }

        }

        if (multiCube)
        {
            cubeTimer += Time.deltaTime;
            if (currentMultiBallSpawnCube >= noOfMultiBalls)
            {
                currentMultiBallSpawnCube = 0;
                multiCube = false;
            }
            else
            {
                if (cubeTimer >= timeInBetweenSpawnCube)
                {
                    SpawnCube(1, true, cubeBuffer);
                    cubeTimer = 0f;
                    //if (currentMultiBallSpawnCube == noOfMultiBalls - 2)
                    //{
                    //    cubeTimer = -2.5f;
                    //}
                    //else if (currentMultiBallSpawnCube == noOfMultiBalls - 1)
                    //{
                    //    cubes.Clear();
                    //    SpawnCube(1, false, cubeBuffer);
                    //}
                    //else
                    //{
                    //    SpawnCube(1, true, cubeBuffer);
                    //    cubeTimer = 0f;
                    //}
                    currentMultiBallSpawnCube++;

                }
            }

        }


        if (currentBalls <= 0)
        {
            ReduceLives();
            SpawnBall(1, spawnPoint.position);
        }

        if (cubes.Count <= 0)
        {
            SpawnCube(1, false, cubeBuffer);
        }

    }

    void HandlePowerUps()
    {
        powerUpTimer += Time.deltaTime;

        if (powerUpTimer > timeInBetweenPowerupSpawn && !isPowerUpInScene)
        {
            timeInBetweenPowerupSpawn = UnityEngine.Random.Range(3f, 9f);
            //powerUpTimer = 0f; Handled in Questionmark script
            Vector2 pointInUnitCircle = UnityEngine.Random.insideUnitCircle * circleRadius;
            Vector3 newSpawnPoint = island.transform.position + island.transform.right * pointInUnitCircle.x + island.transform.forward * pointInUnitCircle.y + island.transform.up * -1f;
            Instantiate(questionMarkPrefab, newSpawnPoint, Quaternion.LookRotation(island.transform.up), island.transform);
            isPowerUpInScene = true;
        }
    }
    void SpawnBall(int numberToSpawn, Vector3 spawnPosition)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            ballSpawnAudio.Play();
            GameObject newBall = Instantiate(playerPrefab, spawnPosition, spawnPoint.rotation);
            balls.Add(newBall);
            currentBalls++;
        }
    }

    public void HandleCubeOutOfTime(GameObject cubeToRemove)
    {
        if (cubes.Count <= 1)
        {
            ReduceLives();
        }
        RemoveCube(cubeToRemove, 0f);
    }

    public void SpawnCube(int numberToSpawn, bool isMultiCube, float buffer)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            cubeSpawnSound.Play();
            GameObject newCube = Instantiate(cubePrefab, GenerateRandomSpawnPoint(), spawnPoint.rotation, island.transform);
            Cube newCubeScript = newCube.GetComponent<Cube>();
            newCubeScript.buffer = buffer;

            if (!isMultiCube)
            {
                cubes.Add(newCube);
                newCubeScript.isMultiCube = false;
            }
            else
            {
                newCubeScript.isMultiCube = true;
                newCube.GetComponent<MeshRenderer>().material = multiCubeMaterial;
            }
        }
    }

    public void RemoveCube(GameObject cubeToRemove, float delay)
    {
        cubes.Remove(cubeToRemove);
        Destroy(cubeToRemove, delay);
    }

    void CheckBallsYPos()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            GameObject ball = balls[i];
            if (ball.transform.position.y < destroyObjectBelowY)
            {
                balls.Remove(ball);
                Destroy(ball, 1);
                currentBalls--;
            }
        }
    }

    void CheckCubesYPos()
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            GameObject cube = cubes[i];
            if (cube.transform.position.y < destroyObjectBelowY)
            {
                RemoveCube(cube, 1f);
            }
        }
    }

    void SpawnMultiBall()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 newSpawnPoint = GenerateRandomSpawnPoint();
            bool isSpawnPointOccupied = Physics.SphereCast(newSpawnPoint, 1f, Vector3.down, out RaycastHit hit, 1f);
            if (!isSpawnPointOccupied)
            {
                SpawnBall(1, newSpawnPoint);
                return;
            }
        }

    }

    public void SpawnCube()
    {
        Vector3 spawnPos = GenerateRandomSpawnPoint();
        Instantiate(cubePrefab, spawnPos, Quaternion.identity, island.transform);
    }

    public void UpdateScore()
    {
        manager.UpdateScore();
    }

    public void ReduceLives()
    {
        manager.ReduceLives();
    }

    Vector3 GenerateRandomSpawnPoint()
    {
        Vector2 pointInUnitCircle = UnityEngine.Random.insideUnitCircle * circleRadius;
        Vector3 newSpawnPoint = spawnPoint.transform.position + new Vector3(pointInUnitCircle.x, 0, pointInUnitCircle.y);
        return newSpawnPoint;
    }

    public void IncreaseBuffer()
    {
        print($"Time cube hit, cube buffer {cubeBuffer}");
        cubeBuffer += 2f;
    }
}
