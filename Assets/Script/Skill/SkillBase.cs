using UnityEngine;

/// <summary>
/// 全てのスキルの基底クラス
/// ScriptableObjectなのでアセットとして作成できるようにする
/// </summary>
public abstract class SkillBase : ScriptableObject
{
    [Header("スキル名")]
    public string SkilName;

    [Header("スキルアイコンUI用")]
    public Sprite icon;

    [Header("クールタイム")]
    public float cooldown;

    [SerializeField, Header("攻撃力")]
    protected float SkillAttackPower;

    [Header("レア度")]
    public SkillRarity rarity;

    [Header("最大レベル")]
    public int maxLevel = 5;

    [Header("現在のレベル")]
    [HideInInspector]
    public int currentLebel = 1;

    /// <summary>
    /// スキルを発動するメソッド
    /// </summary>
    /// <param name="player">プレイヤーの本体にアクセスして攻撃力と位置にアクセスする</param>
    public abstract void Active(Player player);

    /// <summary>
    /// レベルアップ処理
    /// </summary>
    public virtual void LevelUP()
    {
        if(currentLebel < maxLevel)
        {
            currentLebel++;
        }
    }
}