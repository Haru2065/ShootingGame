using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/CreateEnemyData")]
public class EnemySO : ScriptableObject
{
    public List<EnemyStatus> EnemyStatusList;

    private Dictionary<string, EnemyStatus> enemyDictionary;

    /// <summary>
    /// SOが読み込まれた時に自動で辞書を作る
    /// </summary>
    private void OnEnable()
    {
        enemyDictionary = new Dictionary<string, EnemyStatus>();

        foreach (var status in EnemyStatusList)
        {
            if (!enemyDictionary.ContainsKey(status.EnemyID))
            {
                enemyDictionary.Add(status.EnemyID, status);
            }
        }
    }

    /// <summary>
    /// IDを指定して敵データを取得する
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public EnemyStatus GetEnemyStatus(string id)
    {
        if (enemyDictionary.TryGetValue(id, out EnemyStatus status))
        {
            return status;
        }
        Debug.LogWarning($"ID{id}の敵データは見つかりませんでした。");
        return null;
    }

}
