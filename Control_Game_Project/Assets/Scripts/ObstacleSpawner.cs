using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    public float spawnRangeX = 10;
    public float spawnRangeY = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = Random.Range(0, obstacles.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRangeX / 2, spawnRangeX / 2), Random.Range(-spawnRangeY / 2, spawnRangeY / 2), transform.position.z);

            Instantiate(obstacles[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRangeX, spawnRangeY, 1));
    }
}
