using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー本体のスクリプト
/// </summary>
public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player Instance
    {
        get => instance;
    }

    [SerializeField, Header("プレイヤーのIDを検索するために入力")]
    private string playerSearchID;

    [SerializeField, Header("プレイヤーのSO")]
    private PlayerSO playerSO;

    [SerializeField, Header("プレイヤーの体力バー")]
    private Slider playerHPBar;

    [SerializeField, Header("プレイヤーのスプライトレンダラー")]
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField, Header("プレイヤーの無敵時間")]
    private float invincibleTime;

    [SerializeField,Header("UniTaskでの待ち時間")]
    private float delayTime;

    private bool isInvicible;

    //プレイヤーの最大体力
    private int playerMaxHP;

    //プレイヤーの現在体力
    [SerializeField]
    private int playerCurrentHP;

    //キャンセルトークン
    private CancellationTokenSource cts;

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

    private void Start()
    {
        //キャンセルトークンを生成
        cts = new CancellationTokenSource();

        //パラメータの設定処理
        setPlayerParameters();

        //最初は無敵状態にしない
        isInvicible = false;
    }

    /// <summary>
    /// キャンセル処理を実行するメソッド
    /// </summary>
    public void OnCancell()
    {
        cts?.Cancel();
        cts?.Dispose();
    }

    /// <summary>
    /// デバック処理
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            playerCurrentHP = 0;
            playerHPBar.value = playerCurrentHP;
            
            if (playerCurrentHP <= 0 || playerHPBar.value <= 0)
            {
                playerCurrentHP = 0;
                playerHPBar.value = 0;

                GameManager.Instacne.DownRemaingLife();

                //無敵状態を開始
                isInvicible = true;

                //点滅を開始
                StartInvincibleAsync(cts.Token).Forget();

                //無敵を解除
                isInvicible = false;
            }
        }
    }
   

    /// <summary>
    /// パラメータをSOから読み込んで設定するメソッド
    /// </summary>
    private void setPlayerParameters()
    {
        //プレイヤーのSOデータをIDで検索し取得(失敗したらコンソールビューに表示)
       var playerdata = playerSO.GetPlayerStatus(playerSearchID);

        //プレイヤーのデータがあればパラメータを設定
        if(playerdata != null)
        {
            //プレイヤーの最大体力と現在の体力をプレイヤーのSOの最大体力に設定
            playerMaxHP = playerdata.PlayerMaxHP;
            playerCurrentHP = playerMaxHP;

            //プレイヤーの体力バーも最大に設定
            playerHPBar.maxValue = playerdata.PlayerMaxHP;
            playerHPBar.value = playerCurrentHP;
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage">敵の攻撃力分</param>
    public void PlayerOnDamage(int damage)
    {
        // 無敵中なら無視
        if (isInvicible) return;

        playerCurrentHP -= damage;

        playerHPBar.value = playerCurrentHP;

        if(playerCurrentHP <= 0)
        {
            playerCurrentHP = 0;

            playerHPBar.value = playerCurrentHP;

            GameManager.Instacne.DownRemaingLife();

            

            //無敵状態を開始
            isInvicible = true;

            //点滅を開始
            StartInvincibleAsync(cts.Token).Forget();
            
            //無敵を解除
            isInvicible = false;
        }
    }

    private async UniTaskVoid StartInvincibleAsync(CancellationToken token)
    {
        //無敵状態開始
        isInvicible = true;

        try
        {
            //プレイヤーの点滅開始
            await BlinkAsync(token);
        }

        //キャンセル処理が行われたらデバックログに表示
        catch(OperationCanceledException)
        {
            Debug.Log("点滅処理がキャンセルされました");
        }

        //失敗しても必ずスプライトレンダを表示し無敵は解除した状態にする
        finally
        {
            playerSpriteRenderer.enabled = true;
            isInvicible = false;
        }
    }


    /// <summary>
    /// UniTaskプレイヤーの点滅メソッド
    /// </summary>
    /// <param name="token">キャンセルできるメソッド</param>
    /// <returns>点滅間隔</returns>
    private async UniTask BlinkAsync(CancellationToken token)
    {

        float elapsed = 0f;
        while(elapsed < invincibleTime)
        {
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime),cancellationToken: token);
            elapsed += 0.1f;
        }
        
        //最初の表示除隊に戻す
        playerSpriteRenderer.enabled = true;
    }

}
