using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("敵の出現位置")]
    private Transform[] spawnPoints;

    private Coroutine spawnCorotine;

    public void Start()
    {
        
    }

    /// <summary>
    /// WaveManager からスポーン開始要求が来る
    /// </summary>
    public void StartSpawn(WaveStatus wave)
    {
        if(spawnCorotine != null)
        {
            StopCoroutine(spawnCorotine);
        }

        spawnCorotine = StartCoroutine(SpawnCoroutine(wave));
    }

    private IEnumerator SpawnCoroutine(WaveStatus wave)
    {
        for (int i = 0; i < wave.SpawnCount; i++)
        {
            Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(wave.EnemyPrefab,p.position, Quaternion.identity);

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }
}
