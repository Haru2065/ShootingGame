using UnityEngine;

/// <summary>
/// ベースの弾本体のスクリプト
/// </summary>
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("弾の移動速度")]
    protected float bulletSpeed;

    /// <summary>
    /// 弾の速度のゲッターセッター
    /// </summary>
    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    // Update is called once per frame
    void Update()
    {
        ShotMove();
    }

    /// <summary>
    /// ベース弾の移動処理
    /// </summary>
    protected virtual void ShotMove()
    {
        transform.Translate(Vector2.up *  bulletSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 抽象メソッド
    /// </summary>
    /// <param name="other">弾に当たる対象者</param>
    protected abstract void OnTriggerEnter2D(Collider2D other);
}
