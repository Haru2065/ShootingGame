using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵本体のスクリプト
/// </summary>
public class Enemy : MonoBehaviour
{
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
        Debug.Log($"衝突検知: {other.name}");

        if (other.CompareTag("PlayerBullet"))
        {
            Debug.Log("PlayerBullet に命中！");
            WaveManger.Instance.UpDateWave();
            Destroy(gameObject);
        }
    }
}
