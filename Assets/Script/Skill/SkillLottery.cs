using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// Waveクリア時に表示するスキルを抽選するクラス
/// </summary>
public static class SkillLottery
{
    public static List<SkillBase> DrawSkills(
        bool isBossWave,PlayerSkillHolder holder,SkillDataBase database)
    {
        List<SkillBase> result = new();

        //ボスWaveの場合、Epicを最低１枠確定
        if (isBossWave)
        {
            result.Add(GetRandomEpic(database));
        }

        //最大3枠になるまで抽選
        while (result.Count < 3)
        {
            SkillBase skill = DrawOne(holder, database);

            //重複を防止
            if (!result.Contains(skill))
            {
                result.Add(skill);
            }
        }

        return result;  
    }

    /// <summary>
    /// 1枠分のスキルを抽選する
    /// </summary>
    /// <returns></returns>
    private static SkillBase DrawOne(PlayerSkillHolder holder, SkillDataBase database)
    {
        //ランダムで抽選する変数生成
        float rand = Random.value;

        //最高レアリティの確率
        if(rand < 0.15)
        {
            return GetRandomEpic(database);
        }

        //Rare（アクティブスキルの強化)
        if (rand < 0.45 && holder.activeSkill != null)
        {
           return holder.activeSkill;
        }

        return GetRandomCommon(database);
    }

    /// <summary>
    /// Epicスキルをランダムで取得
    /// </summary>
    /// <param name="database"></param>
    /// <returns></returns>
    private static SkillBase GetRandomEpic(SkillDataBase database)
    {
        var list = database.skillList.FindAll(skillList => skillList.rarity == SkillRarity.Epic);

        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Commonスキルをランダムで取得
    /// </summary>
    /// <param name="dataBase"></param>
    /// <returns></returns>
    private static SkillBase GetRandomCommon(SkillDataBase dataBase)
    {
        var list = dataBase.skillList.FindAll(skillList => skillList.rarity == SkillRarity.common);

        return list[Random.Range(0,list.Count)];
    }
}
