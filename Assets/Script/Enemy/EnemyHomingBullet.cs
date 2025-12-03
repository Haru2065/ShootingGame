using UnityEngine;

public class EnemyHomingBullet : Bullet
{

    [SerializeField, Header("回転速度（追尾の敏感さ）")]
    private float rotateSpeed = 200f;

    private Transform target;

    [SerializeField, Header("追跡弾専用攻撃力")]
    private float HomingAttackPower;

    [SerializeField, Header("追尾する秒数")]
    private float homingDuration;

    //追跡する時間
    private float homingTimer;

    //追跡中か
    private bool isHoming;

    //敵本体の攻撃力を保存する変数
    private int enemyBodyAttackPower;

    private void Start()
    {
        // タグでプレイヤーを取得
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;

            isHoming = true;
        }
    }

    /// <summary>
    /// 敵本体の攻撃力を保存するメソッド
    /// </summary>
    /// <param name="power">敵本体の攻撃力</param>
    public void SetOwenerAttackPower(int power)
    {
        enemyBodyAttackPower = power;
    }

    private void Update()
    {

        //追跡開始した時に追跡時間を計測開始
        if (isHoming)
        {
            //追跡時間をリセット
            homingTimer = 0f;

            homingTimer += Time.deltaTime;

            //追跡狩猟時間になったら追跡フラグをfalse
            if (homingTimer >= homingDuration)
            {
                isHoming = false;
            }
        }

        //ターゲットがありかつ追跡中なら実行
        if (isHoming && target != null)
        {
            // プレイヤーの方向を取得
            Vector2 direction = (target.position - transform.position).normalized;

            // 現在の向きをゆっくりプレイヤー方向に回す
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // 前方へ進む
            transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        }

        //追跡終了したら弾を消去」
        else if(!isHoming)
        {
            Destroy(gameObject);
        }
    }

    private void HomingCalculationAttack()
    {
        var result = enemyBodyAttackPower * HomingAttackPower;

        Player.Instance.PlayerOnDamage((int)result);

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HomingCalculationAttack();

            // プレイヤーに当たった時の処理
            Destroy(gameObject);
        }

        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
