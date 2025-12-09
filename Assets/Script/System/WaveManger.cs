using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウェーブを管理するマネージャー
/// </summary>
public class WaveManger : MonoBehaviour
{
    private static WaveManger instance;

    public static WaveManger Instance
    {
        get => instance;
    }

    [SerializeField, Header("WaveData")]
    private WaveData waveData;

    [SerializeField,Header("スポナー参照")]
    private EnemySpawner enemySpawner;



    //Wave数(表示用で動作するWaveCountIndex
    private int currentWaveNumber;

    public int CurrentWaveNumber
    {
        get => currentWaveNumber;
    }

    //Wave数(内部敵に動作するWaveCountIndex）
    private int currentWaveIndex;

    public int CurrentWaveIndex
    {
        get => currentWaveIndex;
    }

    //倒した敵の数
    private int destroyEnemies;

    /// <summary>
    /// 倒した敵の数のゲッターセッター
    /// </summary>
    public int DestroyEnemies
    {
        get => destroyEnemies;
        set => destroyEnemies = value;
    }

    private bool isWaveActive;

    public bool IsWaveActive
    {
        get => isWaveActive;
    }

    //敵の生存数
    private int enemyAliveCount;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //倒した敵の数を0で初期化
        destroyEnemies = 0;

        //WaveIndexを0で初期化
        currentWaveIndex = 0;

        currentWaveNumber = 1;

        StartWave();
    }

    /// <summary>
    /// Wave開始メソッド
    /// </summary>
    private void StartWave()
    {
        if (currentWaveIndex >= waveData.waves.Count)
        {
            Debug.Log("全Wave終了");
            GameManager.Instacne.GameClear();
            return;
        }

        isWaveActive = true;
        destroyEnemies = 0;

        var wave = waveData.waves[currentWaveIndex];

        // Wave 表示更新
        GameManager.Instacne.UpdateWaveText(currentWaveNumber);

        // ★ EnemySpawner にスポーン指示！
        enemySpawner.StartSpawn(wave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Waveを記録するメソッド
    /// </summary>
    public void UpDateWave()
    {
        if(!isWaveActive    ) return;

        //敵を倒したらWave数を増やす
        destroyEnemies++;

        var currentWave = waveData.waves[currentWaveIndex];

        //もし倒した数がクリアに必要な撃破数に達成したらWaveクリアメソッドを実行
        if (currentWave.IsCleared(destroyEnemies))
        {
            WaveClear();
        }
    }

    /// <summary>
    /// ウェーブクリアメソッド
    /// </summary>
    private void WaveClear()
    {
        isWaveActive = false;

        //Wave数を増やす
        currentWaveIndex++;
        currentWaveNumber++;

        //もし次のWaveに行くと次のWaveのスタート処理を実行
        if (currentWaveIndex < waveData.waves.Count)
        {
            //Waveスタート処理
            StartCoroutine(NextWaveDelay());

            //右上のWavetextを更新
            GameManager.Instacne.UpdateWaveText(currentWaveIndex);
        }

        //全てのWaveが終わるとゲーム
        else
        {
            GameManager.Instacne.GameClear();
        }
    }

    /// <summary>
    /// 次のWaveを開始する準備コールチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextWaveDelay()
    {
        var delay = waveData.waves[currentWaveIndex - 1].nextWaveDelay;
        yield return new WaitForSeconds(delay);
        StartWave();
    }
}
