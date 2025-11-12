using System.Collections;
using System.Collections.Generic;
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



    // Update is called once per frame
    void Update()
    {
        //スペースキーを押すとプレイヤーの前の弾の発射位置に弾をスポーンする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(playerBulletObject, playerShotPoint.position, Quaternion.identity);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Instantiate(playerBulletObject, playerShotPoint.position, Quaternion.identity);
        }
    }
}
