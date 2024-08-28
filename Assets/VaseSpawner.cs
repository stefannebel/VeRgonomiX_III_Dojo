using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VaseSpawner : MonoBehaviour
{
    public GameObject[] vasePrefabs; // Array to hold the vase prefabs
    public Transform player; // Reference to the player's transform
    public float radius = 1f; // Distance from the player to the vases
    public int numberOfVases = 6; // Number of vases to spawn
    public float minDistanceFromPlayer = 0.2f; // Minimum distance from the player

    public List<GameObject> vaseList;

    private List<GameObject> spawnedVases = new List<GameObject>();

    private void Start()
    {
        vaseList = new List<GameObject>();
    }

    public void SetRadius(string radius)
    {
        if (float.TryParse(radius, out float parsedRadius))
        {
            this.radius = parsedRadius;
        }
    }
    public void SetMinDistanceFromPlayer(string radius)
    {
        if (float.TryParse(radius, out float parsedRadius))
        {
            minDistanceFromPlayer = parsedRadius;
        }
    }




    private void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame) SpawnFormation(0);
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SpawnFormation(1);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SpawnFormation(2);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SpawnFormation(3);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SpawnFormation(4);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) SpawnFormation(5);
    }

    public void SpawnFormation(int formationType)
    {
        // Clear old spawns
        ClearOldSpawns();

        // Determine which formation to spawn
        switch (formationType)
        {
            case 0:
                SpawnCircle();
                break;
            case 1:
                SpawnLine();
                break;
            case 2:
                SpawnGrid();
                break;
            case 3:
                SpawnRectangle();
                break;
            case 4:
                SpawnTriangle();
                break;
            case 5:
                SpawnRandom();
                break;
                // Add more cases for different formations
        }
    }

    private void ClearOldSpawns()
    {
        foreach (GameObject vase in spawnedVases)
        {
            Destroy(vase);
        }

        foreach (GameObject vase in vaseList)
        {
            Destroy(vase);
        }

        spawnedVases.Clear();
    }

    private void SpawnCircle()
    {
        float angleStep = 360f / numberOfVases;

        for (int i = 0; i < numberOfVases; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = GetPositionOnCircle(player.position, radius, angle);

            GameObject prefabToSpawn = vasePrefabs[i % vasePrefabs.Length];
            GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedVases.Add(spawnedVase);
        }
    }

    private Vector3 GetPositionOnCircle(Vector3 center, float radius, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = center.x + radius * Mathf.Cos(radian);
        float z = center.z + radius * Mathf.Sin(radian);
        return new Vector3(x, center.y, z); // Assuming the vases should be at the same height as the player
    }

    private void SpawnLine()
    {
        float step = 2f * radius / (numberOfVases - 1); // Distance between each vase in the line

        for (int i = 0; i < numberOfVases; i++)
        {
            Vector3 spawnPosition = new Vector3(player.position.x - radius + (step * i), player.position.y, player.position.z);

            // Ensure the position is at least minDistanceFromPlayer away from the player
            if (Vector3.Distance(player.position, spawnPosition) < minDistanceFromPlayer)
            {
                spawnPosition += new Vector3(minDistanceFromPlayer, 0, minDistanceFromPlayer);
            }

            GameObject prefabToSpawn = vasePrefabs[i % vasePrefabs.Length];
            GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedVases.Add(spawnedVase);
        }
    }

    private void SpawnGrid()
    {
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(numberOfVases));
        float step = 2f * radius / (gridSize - 1); // Distance between each vase in the grid

        for (int i = 0; i < numberOfVases; i++)
        {
            int row = i / gridSize;
            int col = i % gridSize;
            Vector3 spawnPosition = new Vector3(player.position.x - radius + (step * col), player.position.y, player.position.z - radius + (step * row));

            // Ensure the position is at least minDistanceFromPlayer away from the player
            if (Vector3.Distance(player.position, spawnPosition) < minDistanceFromPlayer)
            {
                spawnPosition += new Vector3(minDistanceFromPlayer, 0, minDistanceFromPlayer);
            }

            GameObject prefabToSpawn = vasePrefabs[i % vasePrefabs.Length];
            GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedVases.Add(spawnedVase);
        }
    }

    private void SpawnRectangle()
    {
        int rows = numberOfVases / 2;
        int cols = numberOfVases / rows;
        float stepX = 2f * radius / (cols - 1);
        float stepZ = 2f * radius / (rows - 1);

        for (int i = 0; i < numberOfVases; i++)
        {
            int row = i / cols;
            int col = i % cols;
            Vector3 spawnPosition = new Vector3(player.position.x - radius + (stepX * col), player.position.y, player.position.z - radius + (stepZ * row));

            // Ensure the position is at least minDistanceFromPlayer away from the player
            if (Vector3.Distance(player.position, spawnPosition) < minDistanceFromPlayer)
            {
                spawnPosition += new Vector3(minDistanceFromPlayer, 0, minDistanceFromPlayer);
            }

            GameObject prefabToSpawn = vasePrefabs[i % vasePrefabs.Length];
            GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedVases.Add(spawnedVase);
        }
    }

    private void SpawnTriangle()
    {
        int rows = (int)Mathf.Ceil(Mathf.Sqrt(2 * numberOfVases));
        int index = 0;

        for (int row = 0; row < rows; row++)
        {
            int cols = row + 1;
            float step = 2f * radius / (rows - 1);

            for (int col = 0; col < cols && index < numberOfVases; col++)
            {
                Vector3 spawnPosition = new Vector3(player.position.x - radius + (step * col) - (row * step / 2), player.position.y, player.position.z - radius + (step * row));

                // Ensure the position is at least minDistanceFromPlayer away from the player
                if (Vector3.Distance(player.position, spawnPosition) < minDistanceFromPlayer)
                {
                    spawnPosition += new Vector3(minDistanceFromPlayer, 0, minDistanceFromPlayer);
                }

                GameObject prefabToSpawn = vasePrefabs[index % vasePrefabs.Length];
                GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedVases.Add(spawnedVase);
                index++;
            }
        }
    }

    private void SpawnRandom()
    {
        for (int i = 0; i < numberOfVases; i++)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = new Vector3(
                    player.position.x + Random.Range(-radius, radius),
                    player.position.y,
                    player.position.z + Random.Range(-radius, radius)
                );
            } while (Vector3.Distance(player.position, spawnPosition) < minDistanceFromPlayer);

            GameObject prefabToSpawn = vasePrefabs[i % vasePrefabs.Length];
            GameObject spawnedVase = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawnedVases.Add(spawnedVase);
        }
    }
}
