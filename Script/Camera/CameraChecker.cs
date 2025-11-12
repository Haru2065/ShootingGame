using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾が画面外に行ったときに消去するスクリプト
/// </summary>
public class CameraChecker : MonoBehaviour
{
    enum Mode
    {
        None,
        Render,
        RenderOut,
    }

    private Mode mode;  

    // Start is called before the first frame update
    void Start()
    {
        mode = Mode.None;
    }

    // Update is called once per frame
    void Update()
    {
        BulletDestroy();
    }

    /// <summary>
    /// カメラ写っている間は実行
    /// </summary>
    private void OnWillRenderObject()
    {
        //カメラの名前がMainCameraだったらModeをRenderにする
        if (Camera.current.name == "Main Camera")
        {
            mode = Mode.Render;
        }
    }

    /// <summary>
    /// 弾の消去処理
    /// </summary>
    private void BulletDestroy()
    {
        if (mode == Mode.RenderOut)
        {
            Destroy(gameObject);
        }
        else if (mode == Mode.Render)
        {
            mode = Mode.RenderOut;
        }
    }


}
