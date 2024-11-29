using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;  // Array of obstacle prefabs
    public Transform spawnPoint;         // Spawn location
    public float spawnInterval = 2f;     // Time between spawns
    public int maxObstacles = 100;         // Max obstacles in the queue

    private Queue<GameObject> obstacleQueue;  // Queue for obstacle pooling

    void Start()
    {
        // Initialize the Queue
        obstacleQueue = new Queue<GameObject>();

        // Preload obstacles
        for (int i = 0; i < maxObstacles; i++)
        {
            GameObject obstacle = Instantiate(GetRandomObstacle());
            obstacle.SetActive(false);  // Start inactive
            obstacleQueue.Enqueue(obstacle);
        }

        // Start spawning obstacles
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        MoveObstacles();  // Continuously move active obstacles
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacle()
    {
        if (obstacleQueue.Count > 0)
        {
            // Dequeue an obstacle from the pool
            GameObject obstacle = obstacleQueue.Dequeue();

            // Activate and place at the spawn point
            obstacle.transform.position = spawnPoint.position;
            obstacle.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Obstacle Queue is empty! Consider increasing queue size.");
        }
    }

    void MoveObstacles()
    {
        foreach (GameObject obstacle in obstacleQueue)
        {
            if (obstacle.activeSelf)
            {
                obstacle.transform.Translate(Vector3.left * Time.deltaTime * 5f);  // Move left

                // Deactivate and recycle if off-screen
                if (obstacle.transform.position.x < -10f)
                {
                    RecycleObstacle(obstacle);
                }
            }
        }
    }

    void RecycleObstacle(GameObject obstacle)
    {
        obstacle.SetActive(false);
        obstacleQueue.Enqueue(obstacle);  // Return to the queue
    }

    GameObject GetRandomObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        return obstaclePrefabs[index];
    }
}
