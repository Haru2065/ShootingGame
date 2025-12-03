using System;
using UnityEngine;

/// <summary>
/// 各ウェーブにスポーンさせる敵の管理をするステータス
/// </summary>
[Serializable]
public class 
EnemySpawnData
{
    // この敵のプレハブ
    public GameObject EnemyPrefab;

    // 同時に出す数
    public int Count;
}
