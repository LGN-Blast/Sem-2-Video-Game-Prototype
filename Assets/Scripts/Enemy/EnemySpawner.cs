using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;

    void Start()
    {
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (currentWaveCount < waves.Count)
        {
            Wave wave = waves[currentWaveCount];
            CalculateWaveQuota(wave);

            while (wave.spawnCount < wave.waveQuota)
            {
                SpawnEnemies(wave);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            currentWaveCount++;
        }
    }

    void CalculateWaveQuota(Wave wave)
    {
        int total = 0;
        foreach (var group in wave.enemyGroups)
            total += group.enemyCount;

        wave.waveQuota = total;
    }

    void SpawnEnemies(Wave wave)
    {
        foreach (var group in wave.enemyGroups)
        {
            if (group.spawnCount < group.enemyCount)
            {
                Vector2 spawnPosition = new Vector2(
                    player.transform.position.x + Random.Range(-10f, 10f),
                    player.transform.position.y + Random.Range(-10f, 10f));

                Instantiate(group.enemyPrefab, spawnPosition, Quaternion.identity);

                group.spawnCount++;
                wave.spawnCount++;
            }
        }
    }
}
