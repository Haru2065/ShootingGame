using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのスキル所持構造を管理するクラス
/// </summary>
public class PlayerSkillHolder : MonoBehaviour
{
    [Header("アクティブスキル")]
    public SkillBase activeSkill;

    [Header("所持パッシブ")]
    public List<PassiveSkillBase> passiveSkills = new();

    /// <summary>
    /// パッシブスキルを追加、もしくはレベルアップする
    /// </summary>
    /// <param name="skill"></param>
    public void AddOrLevelUpPassive(PassiveSkillBase skill)
    {
        //すでに所持しているかをチェックする
        var owned = passiveSkills.Find(passiveskill => passiveskill.name == skill.name);
        
        //所持済みならレベルアップする
        if (owned != null)
        {
            owned.LevelUP();
        }

        //未所持なら新規追加
        else
        {
            passiveSkills.Add(Instantiate(skill));
        }

    }
}
