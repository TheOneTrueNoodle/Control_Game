using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    public float spawnRangeX = 10;
    public float spawnRangeY = 10;

    [Header("")]
    public GameObject[] obstacles;

    public float spawnRate;

    private bool canSpawn = true;

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        canSpawn = false;
        float timeToNextSpawn = 1 / spawnRate;

        int randomIndex = Random.Range(0, obstacles.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRangeX / 2, spawnRangeX / 2), Random.Range(-spawnRangeY / 2, spawnRangeY / 2), transform.position.z);

        Instantiate(obstacles[randomIndex], randomSpawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(timeToNextSpawn);
        canSpawn = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRangeX, spawnRangeY, 1));
    }
}
