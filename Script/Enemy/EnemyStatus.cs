using System;
using UnityEngine;

[Serializable]
public class EnemyStatus
{
    [Header("“G‚ÌID")]
    public String EnemyID;

    [Header("“G‚Ì–¼‘O")]
    public string EnemyName;

    [Header("“G‚ÌŒ©‚½–Ú")]
    public Sprite EnemySprite;

    [Header("“G‚ÌÅ‘å‘Ì—Í")]
    public int MaxEnemyMaxHP;

    [Header("“G‚ÌUŒ‚—Í")]
    public int EnemyAttackPower;
}
