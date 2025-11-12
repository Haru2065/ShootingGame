using UnityEngine;

/// <summary>
/// 通常に発射し移動する敵の弾スクリプト
/// </summary>
public class EnemyNormalBullet : Bullet
{
    private Vector2 direction = Vector2.down;
    private Enemy enemy;

    public void SetEnemy(Enemy shooter)
    {
        enemy = shooter;
    }

    protected override void ShotMove()
    {
        //毎フレーム下方向に移動する
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemy != null)
        {
            Destroy(gameObject);

            Player.Instance.PlayerOnDamage(enemy.EnemyAttackPower);
        }
        else return;

    }
}
