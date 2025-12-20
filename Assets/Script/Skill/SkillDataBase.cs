using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内に存在するスキル一覧を管理するSO
/// </summary>
[CreateAssetMenu(fileName = "SkillDataBase", menuName = "Skill/SkillDataBase")]
public class SkillDataBase : ScriptableObject
{
    [Header("選択可能なスキル一覧"), SerializeField]
    public List<SkillBase> skillList;
}
