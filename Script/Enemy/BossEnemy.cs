using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : Enemy
{
    [Header("弾発射設定")]
    [SerializeField, Header("共通の発射スクリプト")]
    private EnemyShooter shooter;

    [SerializeField, Header("敵の攻撃間隔")]
    private float attackInterval;

    [Header("攻撃パターン設定")]
    [SerializeField, Header("攻撃パターン")]
    private EnemyShooter.ShotType[] attacckPatternOrder =
    {
        EnemyShooter.ShotType.Normal,
        EnemyShooter.ShotType.Spread,
        EnemyShooter.ShotType.Homing,
    };

    [Header("ステータス設定")]
    [SerializeField, Header("ボスのHPバー")]
    private Slider bossEnemyHPBar;

    [SerializeField,Header("敵のHPBarをコントロールするスクリプト")]
    private EnemyHPBarController enemyHPBarController;

    //キャンセルトークンソース
    private CancellationTokenSource cts;

    protected override void Start()
    {
        base.Start();

        cts = new CancellationTokenSource();

        //弾の種類をループして攻撃を開始(処理を待たずに開始)
        StartAttackPatternLoop(cts.Token).Forget();
    }

    protected override void setEnemyParameters()
    {
        //敵のSOデータをIDで検索し取得(失敗したらコンソールビューに表示)
        var enemydata = enemySO.GetEnemyStatus(searchEnemyID);

        //敵のデータがあればパラメータを設定
        if (enemydata != null)
        {
            base.setEnemyParameters();

            //ボスのHPを最大に設定
            EnemyCurrentHP = enemydata.MaxEnemyMaxHP;

            //ボスのHPBarも最大に設定
            bossEnemyHPBar.maxValue = EnemyCurrentHP;
            bossEnemyHPBar.value = EnemyCurrentHP;

            Debug.Log(bossEnemyHPBar.value);
        }
    }

    public void Update()
    {
        
    }

    /// <summary>
    /// UniTask弾の種類を攻撃パターンとしてループするメソッド
    /// </summary>
    /// <param name="token">キャンセルできる処理</param>
    /// <returns></returns>
    private async UniTaskVoid StartAttackPatternLoop(CancellationToken token)
    {
        //現在の発射ている弾の種類
        int index = 0;

        try
        {
            //キャンセル処理が行われなかったらループし続ける
            while (!token.IsCancellationRequested)
            {
                var pattern = attacckPatternOrder[index];

                shooter.SetShotType(pattern);
                shooter.Fire();

                await UniTask.Delay(TimeSpan.FromSeconds(attackInterval), cancellationToken: token);

                index = (index + 1) % attacckPatternOrder.Length;
            }
        }
        //キャンセル処理が行い例外が発生したら安全にデバックログに返す
        catch(OperationCanceledException)
        {
            Debug.Log("射撃開始をキャンセルされました");
        }
        
    }

    public void OnDmage(int damage)
    {
        EnemyCurrentHP -= damage;

        bossEnemyHPBar.value = EnemyCurrentHP;

        if(EnemyCurrentHP <= 0)
        {
            // 既にキャンセル済みか、nullでないかを確認してからキャンセル
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();

                // 撃破処理（エフェクト表示などがあれば追加）
                Debug.Log("ボスを撃破しました！");
                WaveManger.Instance.UpDateWave();

                OnDestroy();

                // オブジェクトを破壊
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// プレイヤーの弾が当たったときのメソッド
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //親クラスの当たったときにの処理を呼び出す
        base.OnTriggerEnter2D(other);

        

        
    }

    /// <summary>
    /// オブジェクトが消える時にリソースを開放するメソッド
    /// </summary>
    public override void OnDestroy()
    {
        if (cts != null)
        {
            cts.Dispose();
            cts = null;
        }
    }
}
