using UnityEngine;

/// <summary>
/// 自動発動パッシブスキル
/// </summary>
public abstract class PassiveSkillBase : SkillBase 
{
    [Header("発動間隔")]
    public float interval;

    /// <summary>
    /// スキル発動処理
    /// </summary>
    /// <param name="player"></param>
    public override void Active(Player player)
    {
        //パッシブは手動で発動しないようにする

    }

    /// <summary>
    /// 自動効果処理
    /// </summary>
    /// <param name="player"></param>
    public abstract void Apply(Player player);
}
