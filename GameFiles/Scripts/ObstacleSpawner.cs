using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject cactusPrefab; // Prefab for the cactus obstacle
    public GameObject birdPrefab;   // Prefab for the bird obstacle
    public Transform spawnPoint;   // Spawn position for obstacles
    public float initialSpawnInterval = 2f; // Time between obstacle spawns
    public float spawnIntervalDecrease = 0.05f; // How much to reduce interval over time
    public float minSpawnInterval = 0.8f; // Minimum spawn interval
    public float birdSpawnChance = 0.3f; // Probability of spawning a bird (0 to 1)

    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed at which obstacles move
    public float speedIncreaseInterval = 10f; // Interval to increase obstacle speed
    public float speedIncreaseAmount = 0.5f; // Amount to increase speed
    public float maxMoveSpeed = 20f; // Maximum speed of obstacles

    private float currentSpawnInterval;
    private float timeSinceLastSpawn;
    private float timeElapsed;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        timeElapsed = 0f;
    }

    void Update()
    {
        HandleSpawning();
        IncreaseSpeedOverTime();
    }

    void HandleSpawning()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnObstacle();
            timeSinceLastSpawn = 0f;

            // Decrease spawn interval to make the game harder
            if (currentSpawnInterval > minSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecrease;
            }
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle;

        // Decide whether to spawn a bird or a cactus
        if (Random.value < birdSpawnChance)
        {
            obstacle = Instantiate(birdPrefab, spawnPoint.position + new Vector3(0, Random.Range(2f, 4f), 0), Quaternion.identity);
        }
        else
        {
            obstacle = Instantiate(cactusPrefab, spawnPoint.position, Quaternion.identity);
        }

        // Add movement logic to the obstacle
        ObstacleMovement movement = obstacle.AddComponent<ObstacleMovement>();
        movement.moveSpeed = moveSpeed;
    }

    void IncreaseSpeedOverTime()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= speedIncreaseInterval)
        {
            if (moveSpeed < maxMoveSpeed)
            {
                moveSpeed += speedIncreaseAmount;
            }
            timeElapsed = 0f;
        }
    }
}
