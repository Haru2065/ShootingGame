using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのSO
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/CreatePlayerData")]
public class PlayerSO : ScriptableObject
{
    public List<PlayerStatus> PlayerStatusList;

    private Dictionary<string, PlayerStatus> playerDictionary;

    /// <summary>
    /// SOが読み込まれた時に自動で辞書を作る
    /// </summary>
    private void OnEnable()
    {
        playerDictionary = new Dictionary<string, PlayerStatus>();

        foreach(var status in PlayerStatusList)
        {
            if (!playerDictionary.ContainsKey(status.PlayerID))
            {
                playerDictionary.Add(status.PlayerID, status);
            }
        }
    }

    /// <summary>
    /// IDを指定してプレイヤーデータを取得する
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PlayerStatus GetPlayerStatus(string id)
    {
        if(playerDictionary.TryGetValue(id, out PlayerStatus status))
        {
            return status;
        }
        Debug.LogWarning($"ID{id}のプレイヤーデータは見つかりませんでした。");
        return null;
    }
}
