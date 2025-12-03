using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// プレイヤーの射撃処理
/// </summary>
public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーの弾")]
    private GameObject playerBulletObject;

    [SerializeField]
    [Tooltip("プレイヤーの弾の発射位置")]
    private Transform playerShotPoint;

    // プレイヤー本体の攻撃力を取得
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //スペースキーを押すとプレイヤーの前の弾の発射位置に弾をスポーンする
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Shoot();
            
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Shoot(); 
        }
    }

    void Shoot()
    {
        // 弾を生成
        GameObject bullet = Instantiate(playerBulletObject, playerShotPoint.position, Quaternion.identity);

        // PlayerBullet の Init を呼び出して攻撃力を渡す
        if(player != null)
        {
            var playerbullet = bullet.GetComponent<PlayerBullet>();
            if(playerbullet != null)
            {
                playerbullet.Init(player.PlayerAttackPower);
            }
        }
    }
}
