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

    //キャンセルトークンソース
    private CancellationTokenSource cts;

    protected override void Start()
    {
        base.Start();

        cts = new CancellationTokenSource();
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
            bossEnemyHPBar.maxValue = enemydata.MaxEnemyMaxHP;
            bossEnemyHPBar.value = EnemyCurrentHP;
        }
    }

    private void Release()
    {
        cts?.Cancel();
        cts?.Dispose();
    }

    public void Update()
    {
        
    }

    private async UniTaskVoid StartAttackPatternLoop(CancellationToken token)
    {
        int index = 0;
        try
        {
            while (!token.IsCancellationRequested)
            {
                var pattern = attacckPatternOrder[index];

                shooter.SetShotType(pattern);
                shooter.Fire();

                await UniTask.Delay(TimeSpan.FromSeconds(attackInterval), cancellationToken: token);

                index = (index + 1) % attacckPatternOrder.Length;
            }
        }catch(OperationCanceledException)
        {
            Debug.Log("射撃開始をキャンセルされました");
        }
        
    }
}
