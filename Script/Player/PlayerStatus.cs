using System;
using UnityEngine;

/// <summary>
/// プレイヤーのステータス情報
/// </summary>
[Serializable]
public class PlayerStatus
{
    [Header("プレイヤーのID")]
    public string PlayerID;

    [Header("プレイヤーの名前")]
    public string PlayerName;

    [Header("プレイヤーの最大体力")]
    public int PlayerMaxHP;

    [Header("プレイヤーの攻撃力")]
    public int PlayerAttackPower;

    [Header("プレイヤーの見た目")]
    public Sprite PlayerSprite;
}
