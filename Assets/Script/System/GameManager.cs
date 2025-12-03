using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームのシステムを管理するスクリプト
/// </summary>
public class GameManager : MonoBehaviour
{
    //ゲームマネージャのインスタンス
    private static GameManager instance;

    /// <summary>
    /// ゲームマネージャのインスタンスのゲッター
    /// </summary>
    public static GameManager Instacne
    {
        get => instance;
    }

    [SerializeField, Header("プレイヤーの残機テキスト")]
    private TextMeshProUGUI playerRemainingLifeText;

    [SerializeField,Header("プレイヤーの残機数")]
    private int initialLife;

    //プレイヤーの現在の残機
    private int currentLife;

    /// <summary>
    /// プレイヤーの現在の残機のゲッター
    /// </summary>
    public int CurrentLife
    {
        get => currentLife;
    }

    [SerializeField, Header("現在のWaveテキスト")]
    private TextMeshProUGUI wavetext;

    private void Awake()
    {
        //インスタンスがなければインスタンス化する
        if (instance == null)
        {
            instance = this;

            //ゲームマネージャーはシーンまたいでも消さず保持する
            DontDestroyOnLoad(this);
        }

        //インスタンスがすでにあればオブジェクトを消去する
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //残機を初期化する
        currentLife = initialLife;

        //残機のテキストも更新
        UpdateLifeText();

        
    }

    private void Update()
    {
        if (WaveManger.Instance != null && wavetext != null)
        {
            wavetext.text = "Wave " + WaveManger.Instance.CurrentWaveNumber.ToString();
        }
    }

    public void UpdateWaveText(int currentWave)
    {
        if(wavetext != null)
        {
            wavetext.text = "Wave:" + currentWave.ToString();
        }
    }

    /// <summary>
    /// 残機を減らすメソッド
    /// </summary>
    public void DownRemaingLife()
    {
        //残機を減らす
        currentLife--;

        //テキストを更新する
        UpdateLifeText();

        //もし残機0になったらゲームオーバーを実行
        if (currentLife <= 0)
        {
            currentLife = 0;

            Player.Instance.PlayerSpriteRender.enabled = false;

            StartCoroutine(Player.Instance.PlayerDieEffect());

            //GameOver();
        }
    }

    /// <summary>
    /// 残機のテキストを更新するメソッド
    /// </summary>
    public void UpdateLifeText()
    {
        playerRemainingLifeText.text = "RemainingLife:" + currentLife.ToString();
    }

    /// <summary>
    /// ゲームクリアメソッド
    /// </summary>
    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }

    /// <summary>
    /// ゲームオーバー処理を行うメソッド
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
