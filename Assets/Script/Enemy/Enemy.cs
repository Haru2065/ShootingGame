using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵本体のスクリプト
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField, Header("敵の爆発エフェクト")]
    protected GameObject enemyExprosionEffect;

    [SerializeField, Header("敵の爆発エフェクトを生成する位置")]
    protected Transform enemyEffectSpawnPoint;

    [SerializeField,Header("敵のレンダー")]
    protected SpriteRenderer enemyRender;

    private static Enemy instance;

    public static Enemy Instance
    {
        get => instance;            
    }

    //敵の攻撃力
    private int enemyAttackPower;

    /// <summary>
    /// 敵の攻撃力のゲッター
    /// </summary>
    public int EnemyAttackPower
    {
        get => enemyAttackPower;
    }

    private int enemyCurrentHP;

    public int EnemyCurrentHP
    {
        get => enemyCurrentHP;
        set => enemyCurrentHP = value;
    }

    [SerializeField, Header("敵のSO")]
    protected EnemySO enemySO;

    [SerializeField, Header("敵のSOの検索ID")]
    protected string searchEnemyID;

    public string SearchEnemyID
    {
        get => searchEnemyID;
    }
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ベースパラメータを設定するメソッド
    /// </summary>
    protected virtual void Start()
    {
        //敵のパラメータを設定
        setEnemyParameters();

        enemyRender.enabled = true;
    }

    /// <summary>
    /// ベースの敵のパラメータをSOから読み込んで設定するメソッド
    /// </summary>
    protected virtual void setEnemyParameters()
    {
        //敵のSOデータをIDで検索し取得(失敗したらコンソールビューに表示)
        var enemydata = enemySO.GetEnemyStatus(searchEnemyID);

        //敵のデータがあればパラメータを設定
        if (enemydata != null)
        {
            enemyAttackPower = enemydata.EnemyAttackPower;
        }
    }

    /// <summary>
    /// ベースの当たり判定処理
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerBullet"))
        {
            Debug.Log("PlayerBullet に命中！");
            WaveManger.Instance.UpDateWave();

            StartCoroutine(DieWithExplosion());
        }
    }

    /// <summary>
    /// オブジェクトが消える時のメソッド
    /// </summary>
    public virtual void OnDestroy()
    {

    }

    // 爆発エフェクトを表示してから破壊するコルーチン
    protected virtual IEnumerator DieWithExplosion()
    {
        //一度敵のレンダーを非表示
        enemyRender.enabled = false;

        // 爆発エフェクト生成
        yield return StartCoroutine(EnemySpawnExplosion());

        // リソース解放
        OnDestroy();

        // オブジェクト破壊
        Destroy(gameObject);
    }

    protected virtual IEnumerator EnemySpawnExplosion()
    {
        if (enemyExprosionEffect != null && enemyEffectSpawnPoint != null)
        {
            GameObject effectObj = Instantiate(enemyExprosionEffect, enemyEffectSpawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);

            Destroy(effectObj);            
        }

        yield return null;
    }
}
