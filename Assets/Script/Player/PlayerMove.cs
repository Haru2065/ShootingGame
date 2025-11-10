using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動処理
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーの移動速度")]
    private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        //上下左右をWASDで
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
    }
}
