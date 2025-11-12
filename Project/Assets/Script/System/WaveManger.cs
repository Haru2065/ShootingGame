using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    
    [SerializeField,Header("Waveデータ")]
    private WaveData waveData;

    [SerializeField, Header("敵の出現位置")]
    private Transform[] spawnPoints;

    //Wave数(表示用で動作するWaveCountIndex
    private int currentWaveNumber;

    public int CurrentWaveNumber
    {
        get => currentWaveNumber;
    }

    //Wave数(内部敵に動作するWaveCountIndex）
    private int currentwaveIndex;

    //倒した敵の数
    private int destroyEnemies;

    private bool isWaveWctive;

    private bool isSpawning = false;

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
        currentwaveIndex = 0;

        currentWaveNumber = 1;

        StartWave();
    }

    /// <summary>
    /// Wave開始メソッド
    /// </summary>
    private void StartWave()
    {
        if (currentwaveIndex < 0 || currentwaveIndex >= waveData.waves.Count)
        {
            Debug.LogError($"Wave index {currentwaveIndex} is out of range!");
            return;
        }

        isWaveWctive = true;
        destroyEnemies = 0;

        var wave = waveData.waves[currentwaveIndex];

        GameManager.Instacne.UpdateWaveText(currentwaveIndex + 1);


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
        if(!isWaveWctive) return;

        //敵を倒したらWave数を増やす
        destroyEnemies++;

        var currentWave = waveData.waves[currentwaveIndex];

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
        isWaveWctive = false;

        //Wave数を増やす
        currentwaveIndex++;
        currentWaveNumber++;

        //もし次のWaveに行くと次のWaveのスタート処理を実行
        if (currentwaveIndex < waveData.waves.Count)
        {
            //Waveスタート処理
            StartCoroutine(NextWaveDelay());

            //StartWave();

            //右上のWavetextを更新
            GameManager.Instacne.UpdateWaveText(currentwaveIndex);
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
        var delay = waveData.waves[currentwaveIndex - 1].nextWaveDelay;
        yield return new WaitForSeconds(delay);
        StartWave();
    }
}
