using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class WaveStatus
{

    [Header("このWaveで出す敵")]
    public List<EnemySpawnData> enemies = new List<EnemySpawnData>();

    [Header("ウェーブ数")]
    public int waveCount;


    [Header("敵の出現間隔（秒）")]
    public float spawnInterval = 0.5f;

    [Header("次のWaveまでの待機時間")]
    public float nextWaveDelay = 3f;

    [SerializeField,Header("Waveクリアに必要な撃破数")]
    public int WaveClearCount;

    public int SpawnCount;

    /// <summary>
    /// このWaveがクリアされたかどうかを判定
    /// </summary>
    /// <param name="destritedEnemies"></param>
    /// <returns></returns>
    public bool IsCleared(int destritedEnemies)
    {
        return destritedEnemies >= WaveClearCount;
    }
}