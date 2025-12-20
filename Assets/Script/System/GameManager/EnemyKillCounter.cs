using UnityEngine;
using System;

public class EnemyKillCounter : MonoBehaviour
{
    private static EnemyKillCounter instance;

    public static EnemyKillCounter Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //撃破数
    public int TotalKillCount { get; private set; }

    //撃破時のイベント
    public event Action OnEnemyKilled;

    /// <summary>
    /// 敵を倒した時に記録し必ず呼ぶメソッド
    /// </summary>
    public void AddKill()
    {
        TotalKillCount++;
        OnEnemyKilled?.Invoke();
    }
}
