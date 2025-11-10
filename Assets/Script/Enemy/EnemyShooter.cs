using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField, Header("敵の弾プレハブ")]
    private GameObject normalBulletPrefab;

    [SerializeField, Header("扇形の弾のプレハブ")]
    private GameObject spreadBullePrefab;

    [SerializeField, Header("敵の弾が発射される位置")]
    private Transform firePoint;

    [SerializeField, Header("弾が飛ぶカウント")]
    private int bulletCount = 10;

    [SerializeField, Header("弾の角度")]
    private float spreadAngle = 60f;

    [SerializeField, Header("弾の発射種類を選択")]
    private ShotType shotTypeSelect;

    //敵を参照
    private Enemy enemy;
    
    //現在の発射パターン
    private ShotType currentType;

    [SerializeField, Header("パターン攻撃でキャラを動作するか")]
    private bool pattern;

    /// <summary>
    /// 列挙型:弾の発射種類
    /// </summary>
    public enum ShotType
    {
        Normal,
        Spread,
        Homing,
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        
        //パターンで動作させない場合選択した射撃タイプのみで発射する
        if(pattern)
        {
            //弾の種類を選択に応じて発射する弾の種類変える
            currentType = shotTypeSelect;
            
            //選択した射撃タイプで発射
            Fire();
        }
    }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="type">弾の種類</param>
    public void SetShotType(ShotType type)
    {
        currentType = type;
    }

    /// <summary>
    /// 射撃種類を分岐するメソッド
    /// </summary>
    public void Fire()
    {
        switch(currentType)
        {
            //通常弾タイプ
            case ShotType.Normal:

                //通常射撃開始
                FireNormal();
                break;

            //扇形弾タイプ
            case ShotType.Spread:

                //扇形の弾を射撃開始
                FireSpread();
                break;

            //追跡弾タイプ
            case ShotType.Homing:

                //追跡弾を射撃開始
                FireHoming();
                break;
        }
    }

    /// <summary>
    /// 通常の弾の発射処理
    /// </summary>
    public void FireNormal()
    {
        GameObject bulletObj = Instantiate(normalBulletPrefab, firePoint.position, Quaternion.identity);
        EnemyNormalBullet bullet = bulletObj.GetComponent<EnemyNormalBullet>();

        if(bullet != null)
        {
            bullet.SetEnemy(enemy);
        }
    }

    /// <summary>
    /// 扇形の弾を発射するメソッド
    /// </summary>
    private void FireSpread()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // 下方向を基準に扇形に広げる
            Vector2 dir = new Vector2(Mathf.Sin(rad), -Mathf.Cos(rad));

            // 弾を生成
            GameObject bulletObj = Instantiate(spreadBullePrefab, firePoint.position, Quaternion.identity);
            EnemySpreadBullet bullet = bulletObj.GetComponent<EnemySpreadBullet>();
            bullet.SetDirection(dir);

            if (bullet is EnemySpreadBullet spreadBullet)
            {
                spreadBullet.SetEnemy(enemy);
            }
        }
    }
    
    private void FireHoming()
    {
        //GameObject obj = Instantiate(homingBulletPrefab, firePoint.position, Quaternion.identity);
        //var bullet = obj.GetComponent<EnemyHomingBullet>();
        //bullet.SetEnemy(enemy);
    }
}
