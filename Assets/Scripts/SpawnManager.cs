using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 10;
    private float spawnPositionZ = 20;
    private float startDelay = 2.0f;
    public float maxSpawnInterval = 2.5f, minSpawnInterval = 0.01f, smoothingFactor = 28.7f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCouratine());
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetSpawnInterval());
    }

    private IEnumerator SpawnCouratine() {
        yield return new WaitForSeconds(startDelay);

        while (true){
            SpawnRandomAnimal();

            float spawnInterval = GetSpawnInterval();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomAnimal()

    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPositionZ);

        int animalIndex = Random.Range(0, animalPrefabs.Length);

        Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
    }
    private float GetSpawnInterval(){
        float spawnInterval = (maxSpawnInterval - minSpawnInterval) /( (ScoreManager.instance.score / smoothingFactor) + 1) + minSpawnInterval;
        return spawnInterval;
    }
}
