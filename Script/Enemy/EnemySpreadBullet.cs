using UnityEngine;

public class EnemySpreadBullet : Bullet
{
    private Vector2 direction; // 進行方向

    //発射した敵を保持
    private Enemy enemy;

    public void SetEnemy(Enemy shoother)
    {
        enemy = shoother;
    }

    /// <summary>
    /// 外部から進行方向を設定
    /// </summary>
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    protected override void ShotMove()
    {
        // Bulletの基本移動を上書きして、指定方向に進むようにする
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //敵に当たり、タグがプレイヤーなら敵の攻撃力分ダメージを与える
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (enemy != null)
            {
                Player.Instance.PlayerOnDamage(enemy.EnemyAttackPower);
            }
            else
            {
                Debug.LogWarning("Enemy reference is null in bullet!");
            }

           
        }
    }
}
